using Google.Maps.DistanceMatrix;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Model;

namespace UnitOfWorkApplication.API
{
    public class DistanceMatrix
    {
        private const string DistanceMatrixBaseURI = "https://maps.googleapis.com/maps/api/distancematrix/json?";
        private const string DirectionsAPIBaseURI = "https://maps.googleapis.com/maps/api/directions/json?";
        DistanceMatrixService service = new DistanceMatrixService();
        ConcurrentDictionary<int, CabDuration> cabDurations = new ConcurrentDictionary<int, CabDuration>();
        private const string API_KEY = "AIzaSyAM6e58t0Fe-D4zAy6JeHOQ__YjA5BErog";
        private const int MinimumCabDuraiton = 2;
        int cabCount = 0;
        decimal outBoundsFareFactor = 2;

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
                    requestUrl.Append(DistanceMatrixBaseURI);
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
                        var durationValue = (Int32.Parse(responseList.Rows[0].Elements.Skip(cabCount).Take(carTypeDetails.Count()).
                                                                                    Where(x => x.Status == Google.Maps.ServiceResponseStatus.Ok).ToArray().
                                                                                    GroupBy(x => x.duration.Value).Min(x => x.Key)) / 60);
                        durationValue = durationValue == MinimumCabDuraiton ? 2 : durationValue;

                        lstCabDurations.Add(new CabDuration
                                (carType,
                                (durationValue==0?MinimumCabDuraiton:durationValue) + " mins",
                                (durationValue == 0 ? MinimumCabDuraiton : durationValue),
                                carTypeDetails.Select(x=>x.LastLocation).ToList()
                                ));

                        cabCount = cabCount + carTypeDetails.Count();
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
            string cityName = String.Empty;
            var cityCoordiantes = GetCurrentCityCoordinates(model.SourceLocation,out cityName);
            model.RideEstimate.SourceCityName = cityName;
            PointF sourceLocation;
            PointF destinationLocation;
            bool isSourceInsideBounds = false;
            bool isDestinationInsideBounds = false;
            decimal currentStepDistance =0;
            var responseList = GetDistanceDetails(model.SourceLocation, model.DestinationLocation);
            model.DistanceBreakup = new List<DistanceBreakup>();

            int counter = 1;

            foreach(var step in responseList.routes[0].legs[0].steps)
            {               
                if(counter==1)
                {
                     sourceLocation= new PointF(float.Parse(model.SourceLocation.Latitude.ToString()), float.Parse(model.SourceLocation.Longitude.ToString()));
                }
                else
                {
                     sourceLocation= new PointF(float.Parse(responseList.routes[0].legs[0].steps[counter-1].end_location.lat.ToString()), float.Parse(responseList.routes[0].legs[0].steps[counter - 1].end_location.lng.ToString()));
                }
                destinationLocation = new PointF(float.Parse(step.end_location.lat.ToString()), float.Parse(step.end_location.lng.ToString()));
                isSourceInsideBounds = IsPointInPolygon(cityCoordiantes, sourceLocation);
                isDestinationInsideBounds= IsPointInPolygon(cityCoordiantes, destinationLocation);

                if(isSourceInsideBounds && isDestinationInsideBounds)
                {
                    model.DistanceBreakup.Add(new DistanceBreakup() { Position = counter, Value = step.distance.value / 1000, IsInBound = true });
                }
                else if (!isSourceInsideBounds && !isDestinationInsideBounds)
                {
                    model.DistanceBreakup.Add(new DistanceBreakup() { Position = counter, Value = step.distance.value / 1000, IsInBound = false });
                }
                else
                {
                    model.DistanceBreakup.Add(new DistanceBreakup() { Position = counter, Value = step.distance.value / 1000, IsInBound = false });
                }
                counter++;
            }            
            model.RideEstimate.TotalDistance = responseList.routes[0].legs[0].distance.value/1000;

            model.RideEstimate.Duration = responseList.routes[0].legs[0].duration.text;            

            return model;

        }


        private double GetDistanceOffline(double lat1, double lon1, double lat2, double lon2, char unit='K')
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        private GoogleMapDirections GetDistanceDetails(LocationDetailsModel source, LocationDetailsModel destination)
        {
            StringBuilder requestUrl = new StringBuilder();
            requestUrl.Append(DistanceMatrixBaseURI);
            requestUrl.Append("origins=");
            requestUrl.Append(source.Latitude.ToString() + "," + source.Longitude.ToString() + "|");

            requestUrl.Append("&destinations=");
            requestUrl.Append(destination.Latitude.ToString() + "," + destination.Longitude.ToString() + "|");

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

            var responseList = JsonConvert.DeserializeObject<GoogleMapDirections>(responseFromServer);

            return responseList;
        }


        public List<PointF> GetCurrentCityCoordinates(LocationDetailsModel source, out string cityName)
        {
            var coordinateFiles = Directory.GetFiles(@"*.xml");
            cityName = String.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            PointF point;
            List<PointF> coordinates=new List<PointF>();
            foreach (var file in coordinateFiles)
            {

                xmlDoc.Load(file);
                cityName = file.Replace(".xml","");
                coordinates = xmlDoc.SelectNodes(@"coordinates/latlng").Cast<XmlNode>().Select(x => new PointF(float.Parse(x.InnerText.Split(',')[0]), float.Parse(x.InnerText.Split(',')[1]))).ToList();
                point = new PointF(float.Parse(source.Latitude.ToString()), float.Parse(source.Longitude.ToString()));
                if (!IsPointInPolygon(coordinates, point))
                {
                    xmlDoc = new XmlDocument();
                    coordinates = null;
                    cityName = String.Empty;
                }
                else
                {
                    break;
                }
            }
            return coordinates;
        }

        private bool IsPointInPolygon(List<PointF> polygon, PointF point)
        {
            bool isInside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }
    }
}
