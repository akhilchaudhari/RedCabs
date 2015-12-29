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
        DistanceMatrix distanceAPIService;
        IRateCardService rateCardService;    

        public UserService(IUnitOfWork unitOfWork, IUserRepository UserRepository, IRateCardRepository rateCardRepository)
            : base(unitOfWork, UserRepository)
        {
            _unitOfWork = unitOfWork;
            _UserRepository = UserRepository;
            rateCardService = new RateCardService(unitOfWork, rateCardRepository);
        }


        public User GetById(int Id)
        {
            return _UserRepository.GetById(Id);
        }

        public bool CheckDuplicateEntryExists(string key, string value)
        {
            bool result=false;
            switch (key)
            {
                case "Contact":
                    result = this._UserRepository.FindBy(x => x.ContactNo.Equals(value, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null ? true : false;
                    break;
                case "Email":
                    result = this._UserRepository.FindBy(x => x.Email.Equals(value, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null ? true : false;
                    break;

            }
            return result;
        }

        public RideNowModel GetFareEstimate(RideNowModel model)
        {
            distanceAPIService = new DistanceMatrix();
            model = distanceAPIService.GetDistanceDetails(model);
            model.TotalFare = this.rateCardService.GetFare(model.TotalDistance, model.CabType, false, "");
            return model;
        }
    }
}
