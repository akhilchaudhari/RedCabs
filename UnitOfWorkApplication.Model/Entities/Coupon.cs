using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("Coupon")]
    public class Coupon : Entity<int>
    {
        [Required]
        [MaxLength(30)]
        public string CouponCode { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int Value { get; set; }
        
        public virtual CouponValueType CouponValueType { get; set; }
    }
}
