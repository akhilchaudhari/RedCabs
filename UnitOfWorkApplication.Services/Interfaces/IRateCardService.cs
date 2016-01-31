using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Model;

namespace UnitOfWorkApplication.Services.Interfaces
{

    public interface IRateCardService : IEntityService<RateCard>
    {
        RideEstimate GetFare(RideNowModel model, bool isAirportDrop, string sourceCityName);
    }
}
