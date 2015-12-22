using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    public class UserCouponModel
    {
        public int CouponId { get; set; }
        public string Description { get; set; }
        public string CouponCode { get; set; }
    }
}
