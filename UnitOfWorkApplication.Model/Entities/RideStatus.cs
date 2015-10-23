using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("RideStatus")]
    public class RideStatus : Entity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }       
    }
}
