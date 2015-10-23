using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("CarType")]
    public class CarType : Entity<int>
    {

        [Required]
        [MaxLength(15)]
        public string Type { get; set; }       
    }
}
