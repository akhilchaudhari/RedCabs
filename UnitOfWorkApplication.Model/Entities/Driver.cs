using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("Driver")]
    public class Driver : Entity<int>
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string ContactNo { get; set; }

        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int IsAvailable{ get; set; }

        [MaxLength(50)]
        public string LastLocation { get; set; }

        public virtual Car Car { get; set; }

        public string Latitude
        {
            get
            {
                return this.LastLocation.Split(',')[0];
            }
        }

        public string Longitude
        {
            get
            {
                return this.LastLocation.Split(',')[1];
            }
        }
    }
}
