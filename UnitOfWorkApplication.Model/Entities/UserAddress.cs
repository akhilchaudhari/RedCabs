using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
