using Google.Maps.DistanceMatrix;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Model;

namespace UnitOfWorkApplication.API
{
    public class DistanceMatrix
    {
        private const string BaseURI = "https://maps.googleapis.com/maps/api/distancematrix/json?";
        DistanceMatrixService service = new DistanceMatrixService();
        ConcurrentDictionary<int, CabDuration> cabDurations = new ConcurrentDictionary<int, CabDuration>();
        private const string API_KEY = "AIzaSyAM6e58t0Fe-D4zAy6JeHOQ__YjA5BErog";        

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
                    StringBuilder requestUrl = new StringBuilder();                    
                    requestUrl.Append(BaseURI);
                    requestUrl.Append("origins=");
                    requestUrl.Append(latitude.ToString() + "," +  longitude.ToString() + "|");

                    requestUrl.Append("&destinations=");
                    currentDriverList.ForEach(x => requestUrl.Append(x.Latitude.ToString() + "," + x.Longitude.ToString() + "|"));

                    requestUrl.Append("&language=en");

                    requestUrl.Append("&mode=driving");

                    requestUrl.Append("&avoid=none");

                    requestUrl.Append("&units=metric");

                    requestUrl.Append("&key=" + API_KEY);                   

                    WebRequest webRequest = WebRequest.Create(requestUrl.ToString());

                    WebResponse webResponse = webRequest.GetResponse();

                    Stream dataStream = webResponse.GetResponseStream();
                    
                    StreamReader reader = new StreamReader(dataStream);
                    
                    string responseFromServer = reader.ReadToEnd();

                    var responseList = JsonConvert.DeserializeObject<DistanceMatrixResponse>(responseFromServer);

                    counter++;
                   
                    foreach (var carType in currentDriverList.Select(x => x.Car.CarType.Type).Distinct())
                    {
                        var carTypeDetails = currentDriverList.Where(x => x.Car.CarType.Type.Equals(carType));
                        var durationValue = (Int32.Parse(responseList.Rows[0].Elements.Take(carTypeDetails.Count()).
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
                catch(Exception ex)
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

        public RideNowModel GetDistanceDetails(RideNowModel model)
        {          
            StringBuilder requestUrl = new StringBuilder();
            requestUrl.Append(BaseURI);
            requestUrl.Append("origins=");
            requestUrl.Append(model.SourceLocation.Latitude.ToString() + "," + model.SourceLocation.Longitude.ToString() + "|");

            requestUrl.Append("&destinations=");
            requestUrl.Append(model.DestinationLocation.Latitude.ToString() + "," + model.DestinationLocation.Longitude.ToString() + "|");

            requestUrl.Append("&language=en");

            requestUrl.Append("&mode=driving");

            requestUrl.Append("&avoid=none");

            requestUrl.Append("&units=metric");

            requestUrl.Append("&key=" + API_KEY);

            WebRequest webRequest = WebRequest.Create(requestUrl.ToString());

            WebResponse webResponse = webRequest.GetResponse();

            Stream dataStream = webResponse.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            var responseList = JsonConvert.DeserializeObject<DistanceMatrixResponse>(responseFromServer);

            model.TotalDistance = Double.Parse(responseList.Rows[0].Elements[0].distance.Value)/1000;

            model.TotalTime = responseList.Rows[0].Elements[0].duration.Text;

            return model;

        }      
    }
}
