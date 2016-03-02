using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Services.Services
{

    public class DriverService : EntityService<Driver>, IDriverService
    {
        IUnitOfWork _unitOfWork;
        IDriverRepository _driverRepository;

        public DriverService(IUnitOfWork unitOfWork, IDriverRepository driverRepository)
            : base(unitOfWork, driverRepository)
        {
            _unitOfWork = unitOfWork;
            _driverRepository = driverRepository;
        }


        public Driver GetById(int Id)
        {
            return _driverRepository.GetById(Id);
        }

        public List<Driver> GetDriversByCarType(string carType)
        {
            return _driverRepository.GetDriversByCarType(carType);
        }

        public IEnumerable<Driver> GetAllAvailableDrivers()
        {
            return _driverRepository.FindBy(x => x.IsActive && x.IsAvailable==1);
        }

        public IEnumerable<Driver> GetAllActiveDrivers()
        {
            return _driverRepository.FindBy(x => x.IsActive);
        }
    }
}
