using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.API;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Enum;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;

namespace UnitOfWorkApplication.Services.Services
{
    public class RideDetailsService : EntityService<RideDetails>,IRideDetailsService
    {
        IUnitOfWork unitOfWork;
        IDriverService driverService;
        IUserCouponsRepository userCouponsRepository;
        IRideDetailsRepository rideDetailsRepository;

        public RideDetailsService(IUnitOfWork unitOfWork, IRideDetailsRepository rideDetailsRepository, IDriverService driverService, IUserCouponsRepository userCouponsRepository)
             : base(unitOfWork, rideDetailsRepository)
        {
            this.driverService = driverService;
            this.userCouponsRepository = userCouponsRepository;
            this.unitOfWork = unitOfWork;
            this.rideDetailsRepository = rideDetailsRepository;
        }

        public RideNowModel ConfirmRide(RideNowModel model)
        {
            RideDetails details = new RideDetails();
            details.RideDate = SqlDateTime.MinValue.Value;
            details.DestinationAddress = model.DestinationLocation.Address;
            details.DestinationLocation = model.DestinationLocation.Latitude.ToString() + model.DestinationLocation.Longitude.ToString();
            details.isRoundTrip = model.RideEstimate.IsRoundTrip;
            details.NightHalt = model.RideEstimate.NightHalt;
            details.SourceAddress = model.SourceLocation.Address;
            details.SourceLocation = model.SourceLocation.Latitude.ToString() + model.SourceLocation.Longitude.ToString();
            details.User = model.userDetails;
            details.isAirportDrop = model.RideEstimate.IsAirportDrop;
            details.UpdatedOn = DateTime.Now;
            details.RideStatus = RideStatus.Booked.GetHashCode();

            var selectedCoupon = userCouponsRepository.GetById(model.Coupon.Id).FirstOrDefault();

            details.UserCoupon = selectedCoupon!=null?selectedCoupon.Coupon:null;

            var driverList = this.driverService.GetAllAvailableDrivers().ToList();

            DistanceMatrix distanceService = new DistanceMatrix();
            IEnumerable<CabDuration> cabDurations = distanceService.GetCabDurations(model.SourceLocation.Latitude.ToString(),
                                                                                    model.SourceLocation.Longitude.ToString(),
                                                                                    0,
                                                                                    driverList,
                                                                                    model.CabType);
            int driverId = cabDurations.ToList().First().DriverId;

            details.Driver = driverList.Where(x => x.Id == driverId).First();

            this.rideDetailsRepository.Add(details);
            
            this.rideDetailsRepository.Save();  

            model.Ride_Id = details.Id;
            model.ETA = cabDurations.ToList().First().DurationText;
            model.Driver = details.Driver;

            return model;

        }

    }
}
