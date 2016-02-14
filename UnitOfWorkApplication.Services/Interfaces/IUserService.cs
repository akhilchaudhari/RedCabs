using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Model.Enum;

namespace UnitOfWorkApplication.Services.Interfaces
{

    public interface IUserService : IEntityService<User>
    {
        User GetById(int Id);

        DuplicateEntry CheckIfContactExists(string contact);

        DuplicateEntry CheckIfEmailExists(string email);

        User AddUser(User user);

        UserDetails AuthenticateUser(string username, string password);

        RideNowModel GetFareEstimate(RideNowModel model);

    }
}
