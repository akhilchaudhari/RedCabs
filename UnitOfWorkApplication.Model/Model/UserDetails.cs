using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.Model.Model
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string ContactNo { get; set; }

        public bool IsActive { get; set; }

        public string LastLocation { get; set; }

        public int ContactVerificationStatus { get; set; }

        public string Password { get; set; }

        public int AvailableCouponCount{ get; set; }
    }
}
