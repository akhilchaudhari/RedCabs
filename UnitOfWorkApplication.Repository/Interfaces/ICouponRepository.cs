using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Repository.Interfaces
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {       
        Coupon GetDefaultCoupon();  
    }
}
