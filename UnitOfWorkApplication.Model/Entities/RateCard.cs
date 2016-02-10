using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("RateCard")]
    public class RateCard : Entity<int>
    {
        [Required]
        public virtual CarType CarType { get; set; }

        [Required]
        public double MaxDistanceValue { get; set; }

        [Required]
        [MaxLength(32)]
        public string DistanceDisplayText { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        [MaxLength(8)]
        public string DisplayPrice { get; set; }

        [Required]
        public bool IsPerKmPrice { get; set; }

        [Required]
        public bool IsAirportDrop { get; set; }

        [Required]
        public bool Other{ get; set; }

        [Required]
        public int Position { get; set; }
    }
}
