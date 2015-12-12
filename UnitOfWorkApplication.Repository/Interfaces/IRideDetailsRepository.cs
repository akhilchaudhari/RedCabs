using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.Repository.Interfaces
{
    public interface IRideDetailsRepository
    {
        bool BookRide(RideDetails model);
    }
}
