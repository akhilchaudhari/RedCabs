using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("RideStatus")]
    public class RideStatus : Entity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }       
    }
}
