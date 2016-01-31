using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

namespace RideMe.Infrastructure
{
    public class CalculateDistance
    {       
        public void GetCurrentCityCoordinates(float latitude, float longitude)
        {
            var coordinareFiles = Directory.GetFiles(@"C:\Users\akhil\Documents\Visual Studio 2015\Projects\RedCabsWebAPI\RideMe.Infrastructure\Coordinates\");

            XmlDocument xmlDoc = new XmlDocument();
            PointF point;
            List<PointF> coordinates;
            foreach (var file in coordinareFiles)
            {
                xmlDoc.Load(file);
                coordinates = xmlDoc.SelectNodes(@"coordinates/latlng").Cast<XmlNode>().Select(x => new PointF(float.Parse(x.InnerText.Split(',')[0]), float.Parse(x.InnerText.Split(',')[1]))).ToList();
                point = new PointF(latitude,longitude);
                if(!IsPointInPolygon(coordinates, point))
                {
                    xmlDoc = new XmlDocument();
                    coordinates = null;
                }
                else
                {
                    break;
                }
            }                       
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