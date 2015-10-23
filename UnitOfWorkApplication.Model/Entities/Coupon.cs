using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("Coupon")]
    public class Coupon : Entity<int>
    {
        [Required]
        [MaxLength(30)]
        public string CouponCode { get; set; }

        public DateTime ExpiryDate { get; set; }
        
        public virtual IEnumerable<CouponValueType> CouponValueType { get; set; }
    }
}
