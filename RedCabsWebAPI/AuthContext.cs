using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace RedCabsWebAPI
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("DBConnectionString")
        {

        }
    }

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(16)]
        public string PSK { get; set; }
    }
}
