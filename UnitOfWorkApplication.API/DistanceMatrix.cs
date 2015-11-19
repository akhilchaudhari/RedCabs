using Google.Maps.DistanceMatrix;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.API
{
    public class DistanceMatrix
    {
        private const string BaseURI = "https://maps.google.com/maps/api/distancematrix/";
        DistanceMatrixService service = new DistanceMatrixService();
        //List<CabDuration> cabDurations = new List<CabDuration>();
        ConcurrentDictionary<int, CabDuration> cabDurations = new ConcurrentDictionary<int, CabDuration>();
        int i = 0;
        public IEnumerable<CabDuration> GetCabDurations(string latitude, string longitude, List<Driver> drivers, string apiKey)
        {            

            Parallel.ForEach(drivers, driver =>
             {
                 try
                 {
                     DistanceMatrixRequest request = new DistanceMatrixRequest();
                     request.AddDestination(new Google.Maps.Waypoint(Decimal.Parse(latitude), Decimal.Parse(longitude)));
                     request.AddOrigin(new Google.Maps.Waypoint(Decimal.Parse(driver.Latitude), Decimal.Parse(driver.Longitude)));
                     request.Avoid = Google.Maps.Avoid.none;
                     request.Mode = Google.Maps.TravelMode.driving;
                     request.Language = "en";
                     request.Units = Google.Maps.Units.metric;
                     request.Sensor = false;
                     var response = service.GetResponse(request);

                     if (response.Rows[0].Elements[0].Status == Google.Maps.ServiceResponseStatus.Ok)
                     {
                         cabDurations.TryAdd(i,new CabDuration(driver.Car.CarType.Type, response.Rows[0].Elements[0].duration.Text, Int32.Parse(response.Rows[0].Elements[0].duration.Value) / 60));
                         i++;
                     }
                 }
                 catch(Exception ex)
                 {

                 }
             });


            var lstCabDurations = cabDurations.GroupBy(x => x.Value.CarType).Select(x => new CabDuration {CarType = x.Key, DurationValue = x.Min(t => t.Value.DurationValue), DurationText= x.Min(t => t.Value.DurationValue).ToString() + " mins" , Drivers = drivers.Where(t=>t.Car.CarType.Type==x.Key).Select(y=>y.LastLocation).ToList()});

            return lstCabDurations;

            

        }
    }
}
