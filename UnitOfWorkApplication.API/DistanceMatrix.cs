using Google.Maps.DistanceMatrix;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public IEnumerable<CabDuration> GetCabDurations(string latitude, string longitude, List<Driver> drivers)
        {
            int counter = 0;
            List<Driver> currentDriverList;
            List<CabDuration> lstCabDurations = new List<CabDuration>();
            do
            {
                currentDriverList = drivers.OrderBy(x => x.Car.CarType.Type).ThenBy(x => x.Id).Skip((counter) * 25).Take(25).ToList();
                try
                {
                    int driverNumber = 1;
                    counter++;
                    SortedList<int, Google.Maps.Waypoint> driverList = new SortedList<int, Google.Maps.Waypoint>();

                    currentDriverList.ForEach(x => driverList.Add(driverNumber++, new Google.Maps.Waypoint((Decimal.Parse(x.Latitude)), Decimal.Parse(x.Longitude))));
                    DistanceMatrixRequest request = new DistanceMatrixRequest();
                    request.AddOrigin(new Google.Maps.Waypoint(Decimal.Parse(latitude), Decimal.Parse(longitude)));
                    request.WaypointsDestination = driverList;
                    request.Avoid = Google.Maps.Avoid.none;
                    request.Mode = Google.Maps.TravelMode.driving;
                    request.Language = "en";
                    request.Units = Google.Maps.Units.metric;
                    request.Sensor = false;
                    var response = service.GetResponse(request);
                    foreach (var carType in currentDriverList.Select(x => x.Car.CarType.Type).Distinct())
                    {
                        var carTypeDetails = currentDriverList.Where(x => x.Car.CarType.Type.Equals(carType));
                        var durationValue = (Int32.Parse(response.Rows[0].Elements.Take(carTypeDetails.Count()).
                                                                                    Where(x => x.Status == Google.Maps.ServiceResponseStatus.Ok).ToArray().
                                                                                    GroupBy(x => x.duration.Value).Min(x => x.Key)) / 60);
                        lstCabDurations.Add(new CabDuration
                                (carType,
                                durationValue + " mins",
                                durationValue,
                                carTypeDetails.Select(x=>x.LastLocation).ToList()
                                ));
                    }                    
                }
                catch
                {

                }
            } while (drivers.Except(currentDriverList).Take(25).Count() > 0);

            return lstCabDurations.
                    GroupBy(x=>x.CarType).
                    Select(x=> new CabDuration()
                    {
                        CarType =x.Key,
                        Drivers =x.Select(t=>t.Drivers).FirstOrDefault(),
                        DurationText =x.Select(t => t.DurationText).FirstOrDefault(),
                        DurationValue =x.Select(t => t.DurationValue).FirstOrDefault()
                    });



        }
    }
}
