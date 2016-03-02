using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    [Table("User")]
    public class User : Entity<int>
    {       
        
        public string Name { get; set; }

        public string Email { get; set; }
        
        public string ContactNo { get; set; }

        
        public bool IsActive { get; set; }

        public string LastLocation { get; set; }

        
        public int ContactVerificationStatus { get; set; }

        
        public string Password { get; set; }
        
    }
}

