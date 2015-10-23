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

        public virtual RideStatus RideStatus { get; set; }
    }
}