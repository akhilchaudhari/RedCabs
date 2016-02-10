using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Model
{
    public class RateCardModel
    {
        public String CarType { get;set;}

        public String DistanceDisplayText { get; set; }

        public String DisplayPrice { get; set; }

        public bool IsPerKmPrice { get; set; }

        public bool IsAirportDrop { get; set; }
    }
}

