using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("CarType")]
    public class CarType : Entity<int>
    {

        [Required]
        [MaxLength(15)]
        public string Type { get; set; }       
    }
}
