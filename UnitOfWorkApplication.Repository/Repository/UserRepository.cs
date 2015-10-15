using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.Repository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
              : base(context)
        {

        }
        public User GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }       
    }
}
