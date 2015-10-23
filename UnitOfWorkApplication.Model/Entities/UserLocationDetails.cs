using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("UserLocationDetails")]
    public class UserLocationDetails : Entity<int>
    {
        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        public virtual User User { get; set; }
    }
}
