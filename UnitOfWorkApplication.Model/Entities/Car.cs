using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("Car")]
    public class Car : Entity<int>
    {
        [Required]
        [MaxLength(30)]
        public string Model { get; set; }


        [Required]
        [MaxLength(15)]
        public string Color { get; set; }

        public virtual CarType CarType { get; set; }
    }
}
