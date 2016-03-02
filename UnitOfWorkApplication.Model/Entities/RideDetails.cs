using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("RideDetails")]
    public class RideDetails : Entity<int>
    {
        [Required]
        public DateTime UpdatedOn { get; set; }      

        public virtual Driver Driver{ get; set; }

        public virtual User User { get; set; }

        public int RideStatus { get; set; }

        public virtual Coupon UserCoupon { get; set;}

        public string SourceLocation{ get; set; }

        public string SourceAddress { get; set; }

        public string DestinationLocation { get; set; }

        public string DestinationAddress { get; set; }

        public DateTime RideDate { get; set; }

        public decimal BaseFare { get; set; }

        public int NightHalt { get; set; }

        public bool isRoundTrip { get; set; }

        public decimal TotalFare { get; set; }

        public decimal RideRating { get; set; }

        public string Feedback { get; set; }

        public bool isAirportDrop { get; set; }
    }
}