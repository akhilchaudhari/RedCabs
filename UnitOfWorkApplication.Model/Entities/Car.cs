using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("Car")]
    public class Car : Entity<int>
    {
        [Required]
        [MaxLength(30)]
        public string Model { get; set; }


        [Required]
        [MaxLength(15)]
        public string Color { get; set; }

        public virtual CarType CarType { get; set; }
    }
}
