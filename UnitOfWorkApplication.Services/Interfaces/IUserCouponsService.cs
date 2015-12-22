using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Services.Interfaces
{

    public interface IUserCouponsService : IEntityService<UserCoupons>
    {
        List<UserCoupons> GetById(int Id);

        List<UserCoupons> GetActiveCouponsForUser(int userId);
    }
}
