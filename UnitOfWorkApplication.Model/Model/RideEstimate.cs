using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Model
{
    public class RideEstimate 
    {

        public string Duration{ get; set; }
        public double OneWayBaseFare{ get; set; }

        public double TwoWayBaseFare { get; set; }

        public double AirPortFare { get; set; }
        public double TotalDistance{ get; set; }
        public double OutBoundDistance{ get; set; }
        public int NightHalt{ get; set; }

        public double NightHaltCharges { get; set; }
        public double ServiceTax{ get; set; }
        public bool IsRoundTrip{ get; set; }
        public double RoundTripFareFactor{ get; set; }

        public bool IsAirportDrop { get; set; }

        public string SourceCityName { get; set; }

    }

}
