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

    public class UserCouponsService : EntityService<UserCoupons>, IUserCouponsService
    {
        IUnitOfWork _unitOfWork;
        IUserCouponsRepository _UserCouponsRepository;

        public UserCouponsService(IUnitOfWork unitOfWork, IUserCouponsRepository UserCouponsRepository)
            : base(unitOfWork, UserCouponsRepository)
        {
            _unitOfWork = unitOfWork;
            _UserCouponsRepository = UserCouponsRepository;
        }

        public List<UserCoupons> GetById(int Id)
        {
            return _UserCouponsRepository.GetById(Id);
        }

        public List<UserCoupons> GetActiveCouponsForUser(int userId)
        {
            return _UserCouponsRepository.GetActiveCouponsForUser(userId);
        }
    }
}
