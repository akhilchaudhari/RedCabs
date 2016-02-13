using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Model;

namespace UnitOfWorkApplication.Services.Interfaces
{

    public interface IUserService : IEntityService<User>
    {
        User GetById(int Id);

        int CheckIfContactExists(string contact);

        int CheckIfEmailExists(string email);

        User AddUser(User user);

        UserDetails AuthenticateUser(string username, string password);

        RideNowModel GetFareEstimate(RideNowModel model);

    }
}
