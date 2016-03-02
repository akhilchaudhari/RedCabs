using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Repository.Repository
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(DbContext context)
              : base(context)
        {

        }
      
        public Coupon GetDefaultCoupon()
        {
            return FindBy(x => x.CouponCode=="RIDE100").First();
        }
    }
}
