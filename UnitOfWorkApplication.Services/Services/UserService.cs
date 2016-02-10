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

namespace UnitOfWorkApplication.Services.Services
{

    public class UserService : EntityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _UserRepository;
        IUserCouponsRepository _userCouponRepository;
        DistanceMatrix distanceAPIService;
        IRateCardService rateCardService;
        IUserCouponsService userCouponService;        

        public UserService(IUnitOfWork unitOfWork, IUserRepository UserRepository, IRateCardRepository rateCardRepository, IUserCouponsRepository _userCouponRepository)
            : base(unitOfWork, UserRepository)
        {
            _unitOfWork = unitOfWork;
            _UserRepository = UserRepository;
            rateCardService = new RateCardService(unitOfWork, rateCardRepository, _userCouponRepository);
            this._userCouponRepository = _userCouponRepository;
        }


        public User GetById(int Id)
        {
            return _UserRepository.GetById(Id);
        }

        public List<string> CheckDuplicateEntryExists(string contact, string email)
        {
            List<string> duplicateEntities = new List<string>();

            var userList = this._UserRepository.FindBy(x => x.ContactNo.Equals(contact, StringComparison.OrdinalIgnoreCase) || x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            var user = userList.Where(x => x.ContactNo.Equals(contact)).FirstOrDefault();
            if(user!=null)
            {
                duplicateEntities.Add("contact");
            }
            user = userList.Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (user != null)
            {
                duplicateEntities.Add("email");
            }           
            return duplicateEntities;
        }

        public User AddUser(User user)
        {
            user.Coupons = this.userCouponService.GetById(1);
            this._UserRepository.Add(user);
            return user;
        }

        public UserDetails AuthenticateUser(List<KeyValuePair> model)
        {
            List<KeyValuePair> result = new List<KeyValuePair>();
            UserDetails userDetails = new UserDetails();
            String username = model.Where(t => t.Key.Equals("Username")).FirstOrDefault().Value;
            String password = model.Where(t => t.Key.Equals("Password")).FirstOrDefault().Value;
            User userModel = this._UserRepository.FindBy(x => x.IsActive == true
                                                        || x.ContactNo.Equals(username)
                                                        || x.Email.Equals(username)
                                                        || x.Password.Equals(password)).FirstOrDefault();

            if(userModel!=null)
            {

                userDetails.ContactNo = userModel.ContactNo;
                userDetails.ContactVerificationStatus = userModel.ContactVerificationStatus;
                userDetails.Email = userModel.Email;
                userDetails.Id = userModel.Id;
                userDetails.IsActive = userModel.IsActive;
                userDetails.LastLocation = userModel.LastLocation;
                userDetails.Name = userModel.Name;
                userDetails.Password = userModel.Password;

                userCouponService = new UserCouponsService(_unitOfWork, _userCouponRepository);
                userDetails.Coupons = userCouponService.GetActiveCouponsForUser(userModel.Id);
            }                           
            
            return userDetails;
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
