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
    public class UserCouponsRepository : GenericRepository<UserCoupons>, IUserCouponsRepository
    {
        public UserCouponsRepository(DbContext context)
              : base(context)
        {

        }
        public List<UserCoupons> GetById(int id)
        {
            return FindBy(x => x.User.Id == id).ToList();
        }

        public List<UserCoupons> GetActiveCouponsForUser(int userId)
        {
            return FindBy(x => x.User.Id == userId && x.IsActive == true).ToList();
        }
    }
}
