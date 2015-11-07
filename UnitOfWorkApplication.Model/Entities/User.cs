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

    }
}
