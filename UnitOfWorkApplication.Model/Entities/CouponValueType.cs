using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
