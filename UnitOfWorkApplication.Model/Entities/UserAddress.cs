using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("UserAddress")]
    public class UserAddress : Entity<int>
    {
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string AddressType{ get; set; }      
    }
}
