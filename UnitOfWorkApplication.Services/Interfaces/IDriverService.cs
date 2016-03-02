using UnitOfWorkApplication.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Services.Interfaces
{

    public interface IDriverService : IEntityService<Driver>
    {
        Driver GetById(int Id);

        List<Driver> GetDriversByCarType(string carType);

        IEnumerable<Driver> GetAllAvailableDrivers();

        IEnumerable<Driver> GetAllActiveDrivers();
    }
}
