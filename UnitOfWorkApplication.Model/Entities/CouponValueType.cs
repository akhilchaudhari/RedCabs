using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("CouponValueType")]
    public class CouponValueType : Entity<int>
    {
        [Required]
        [MaxLength(15)]
        public string Value { get; set; }

    }
}
