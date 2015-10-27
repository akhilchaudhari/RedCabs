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

    public class CarTypeService : EntityService<CarType>, ICarTypeService
    {
        IUnitOfWork _unitOfWork;
        ICarTypeRepository _carTypeRepository;

        public CarTypeService(IUnitOfWork unitOfWork, ICarTypeRepository carTypeRepository)
            : base(unitOfWork, carTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _carTypeRepository = carTypeRepository;
        }


        public CarType GetById(int Id)
        {
            return _carTypeRepository.GetById(Id);
        }
    }
}
