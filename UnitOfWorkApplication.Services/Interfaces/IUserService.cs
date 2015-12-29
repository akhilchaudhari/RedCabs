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

        bool CheckDuplicateEntryExists(string key, string value);

        RideNowModel GetFareEstimate(RideNowModel model);

    }
}
