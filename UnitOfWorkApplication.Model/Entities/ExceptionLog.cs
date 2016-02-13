using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("ExceptionLog")]
    public class ExceptionLog : Entity<int>
    {

        [Required]
        [MaxLength(64)]
        public string ClassName { get; set; }

        [Required]
        [MaxLength(64)]
        public string MethodName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Tag { get; set; }

        [Required]
        [MaxLength(512)]
        public string ExceptionType { get; set; }

        [Required]
        public string StackTrace { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        [Required]
        public string ObjectDetails { get; set; }

        [Required]
        public bool IsServerException { get; set; }        
    }
}
