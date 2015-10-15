using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;

namespace UnitOfWorkApplication.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetById(int id);
    }
}
