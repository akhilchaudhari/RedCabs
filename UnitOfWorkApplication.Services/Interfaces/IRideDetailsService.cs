using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Model;

namespace UnitOfWorkApplication.Services.Interfaces
{

    public interface IRideDetailsService : IEntityService<RideDetails>
    {
        RideNowModel ConfirmRide(RideNowModel model);
    }
}
