using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;

namespace UnitOfWorkApplication.Services.Services
{
    public class UserService : EntityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }


        public User GetById(int Id)
        {
            return _userRepository.GetById(Id);
        }
    }
}
