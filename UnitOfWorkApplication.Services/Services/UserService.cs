using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.API;
using UnitOfWorkApplication.Model.Enum;

namespace UnitOfWorkApplication.Services.Services
{

    public class UserService : EntityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _UserRepository;
        IUserCouponsRepository _userCouponRepository;
        ILoggerService loggerService;
        DistanceMatrix distanceAPIService;
        IRateCardService rateCardService;

        public UserService(IUnitOfWork unitOfWork, IUserRepository UserRepository, IRateCardRepository rateCardRepository, IUserCouponsRepository userCouponRepository, ILoggerService loggerService)
            : base(unitOfWork, UserRepository)
        {
            _unitOfWork = unitOfWork;
            _UserRepository = UserRepository;
            rateCardService = new RateCardService(unitOfWork, rateCardRepository, _userCouponRepository);
            this._userCouponRepository = userCouponRepository;
            this.loggerService = loggerService;
        }
        public User GetById(int Id)
        {
            return _UserRepository.GetById(Id);
        }

        public DuplicateEntry CheckIfContactExists(string contact)
        {
            DuplicateEntry result;
            try
            {
                var userList = this._UserRepository.FindBy(x => x.ContactNo.Equals(contact, StringComparison.OrdinalIgnoreCase));
                if (userList.Any())
                {
                    result = DuplicateEntry.True;
                }
                else
                {
                    result = DuplicateEntry.False;
                }
            }
            catch (Exception ex)
            {
                result = DuplicateEntry.Error;
                loggerService.Log(new ExceptionLog()
                {
                    MethodName = "CheckIfContactExists",
                    ClassName = this.GetType().Name,
                    ErrorMessage = ex.Message,
                    ExceptionType = ex.GetType().ToString(),
                    IsServerException = true,
                    ObjectDetails = Newtonsoft.Json.JsonConvert.SerializeObject("Email: " + contact),
                    StackTrace = ex.StackTrace,
                    Tag = "Check Duplicate Contact"
                });
            }
            return result;
        }

        public DuplicateEntry CheckIfEmailExists(string email)
        {
            DuplicateEntry result;
            try
            {
                var userList = this._UserRepository.FindBy(x=>x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                if (userList.Any())
                {
                    result = DuplicateEntry.True;
                }
                else
                {
                    result = DuplicateEntry.False;
                }
            }
            catch (Exception ex)
            {
                result = DuplicateEntry.Error;
                loggerService.Log(new ExceptionLog()
                {
                    MethodName = "CheckIfEmailExists",
                    ClassName = this.GetType().Name,
                    ErrorMessage = ex.Message,
                    ExceptionType = ex.GetType().ToString(),
                    IsServerException = true,
                    ObjectDetails = Newtonsoft.Json.JsonConvert.SerializeObject("Email: " + email),
                    StackTrace = ex.StackTrace,
                    Tag = "Check Duplicate Email"
                });
            }
            return result;
        }

        public User AddUser(User user)
        {
          //  user.Coupons = this._userCouponRepository.GetById(1);
            this._UserRepository.Add(user);
            this._UserRepository.Save();
            return user;
        }

        public RideNowModel AuthenticateUser(string username, string password)
        {
            List<KeyValuePair> result = new List<KeyValuePair>();
            RideNowModel model = new RideNowModel();

            model.userDetails = this._UserRepository.FindBy(x => x.IsActive == true
                                                        && ( x.ContactNo.Equals(username)
                                                        || x.Email.Equals(username))
                                                        && x.Password.Equals(password)
                                                        && x.ContactVerificationStatus==2
                                                        && x.IsActive==true).FirstOrDefault();
            model.RideStatus = 1;
            return model;
        }

        public RideNowModel GetFareEstimate(RideNowModel model)
        {
            distanceAPIService = new DistanceMatrix();
            model = distanceAPIService.GetDistanceDetails(model);
            model.RideEstimate = this.rateCardService.GetFare(model, false, "");
            return model;
        }
    }
}
