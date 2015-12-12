using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    public class RideNowModel
    {
        public String Latitude { get;set;}
        public String Longitude { get; set; }
        public int UserId { get; set; }
        public String CabType { get; set; }
    }
}
