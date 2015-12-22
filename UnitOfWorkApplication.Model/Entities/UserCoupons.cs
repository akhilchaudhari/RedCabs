using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("UserCoupons")]
    public class UserCoupons : Entity<int>
    {      
        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Coupon Coupon { get; set; }

        public bool IsActive { get; set; }
    }
}
