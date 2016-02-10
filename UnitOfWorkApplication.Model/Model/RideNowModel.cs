using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.Model.Model
{
    public class RideNowModel
    {
        public RideNowModel()
        {
            this.SourceLocation = new LocationDetailsModel();
            this.DestinationLocation = new LocationDetailsModel();
            this.RideEstimate = new RideEstimate();
            this.DistanceBreakup = new List<Model.DistanceBreakup>();
            this.Coupon = new Coupon();
        }
               
        public User User { get; set; }
        public String CabType { get; set; }    

        public RideStatus RideStatus { get; set; }

        public LocationDetailsModel SourceLocation { get; set; }

        public LocationDetailsModel DestinationLocation { get; set; }

        public RideEstimate RideEstimate { get; set; }

        public List<DistanceBreakup> DistanceBreakup { get; set; }

        public Coupon Coupon { get; set; }

        public Driver Driver;


    }
}
