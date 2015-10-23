using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("DriverLocationDetails")]
    public class DriverLocationDetails : Entity<int>
    {
        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
