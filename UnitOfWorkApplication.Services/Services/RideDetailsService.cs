using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;

namespace UnitOfWorkApplication.Services.Services
{
    public class RideDetailsService : EntityService<RideDetails>,IRideDetailsService
    {
        IUnitOfWork unitOfWork;
        IDriverRepository driverRepository;
        IUserCouponsRepository userCouponsRepository;
        IRideDetailsRepository rideDetailsRepository;

        public RideDetailsService(IUnitOfWork unitOfWork, IRideDetailsRepository rideDetailsRepository, IDriverRepository driverRepository, IUserCouponsRepository userCouponsRepository)
             : base(unitOfWork, rideDetailsRepository)
        {
            this.driverRepository = driverRepository;
            this.userCouponsRepository = userCouponsRepository;
            this.unitOfWork = unitOfWork;
            this.rideDetailsRepository = rideDetailsRepository;
        }

        public Driver ConfirmRide(RideNowModel model)
        {
            RideDetails details = new RideDetails();
            details.DestinationAddress = model.DestinationLocation.Address;
            details.DestinationLocation = model.DestinationLocation.Latitude.ToString() + model.DestinationLocation.Longitude.ToString();
            details.Driver = model.Driver;
            details.isRoundTrip = model.RideEstimate.IsRoundTrip;
            details.NightHalt = model.RideEstimate.NightHalt;
            details.SourceAddress = model.SourceLocation.Address;
            details.SourceLocation = model.SourceLocation.Latitude.ToString() + model.SourceLocation.Longitude.ToString();
            model.User.Id = model.User.Id;
            details.isAirportDrop = model.RideEstimate.IsAirportDrop;

            this.rideDetailsRepository.Add(details);

            return details.Driver;

        }

    }
}
