using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Model
{
    public class RideNowModel
    {
        public String Latitude { get;set;}

        public String Longitude { get; set; }

        public int UserId { get; set; }
        public String CabType { get; set; }    

        public LocationDetailsModel SourceLocation { get; set; }

        public LocationDetailsModel DestinationLocation { get; set; }

        public Double TotalDistance { get; set; }

        public Double TotalFare { get; set; }

        public String TotalTime { get; set; }
    }
}
