using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("User")]
    public class User : Entity<int>
    {       

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string ContactNo { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [MaxLength(30)]
        public string LastLocation { get; set; }

        [Required]
        public int ContactVerificationStatus { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        public List<UserCoupons> Coupons { get; set; }

        [NotMapped]
        public bool IsDuplicateContact { get; set;}

        [NotMapped]
        public bool IsDuplicateEmail { get; set; }
    }
}

