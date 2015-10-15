using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.Services.Interfaces
{
    public interface IUserService : IEntityService<User>
    {
        User GetById(int Id);
    }
}
