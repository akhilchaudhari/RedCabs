using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitOfWorkApplication.API;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Services.Interfaces;

namespace RedCabsWebAPI.Controllers
{
    public class RideNowController : ApiController
    {
        IDriverService driverService;
        ICarTypeService carTypeService;
        IUserService userService;
        IRideDetailsService rideDetailsService;

        public RideNowController(IDriverService driverService, ICarTypeService carTypeService, IUserService userService, IRideDetailsService rideDetailsService)
        {
            this.driverService = driverService;
            this.carTypeService = carTypeService;
            this.userService = userService;
            this.rideDetailsService = rideDetailsService;
        }

        [Authorize]
        [HttpPost]
        public RideNowModel GetRideEstimates(RideNowModel rideNowModel)
        {
            rideNowModel = this.userService.GetFareEstimate(rideNowModel);
            return rideNowModel;
        }

        [HttpPost]
        public RideNowModel ConfirmRide(RideNowModel rideNowModel)
        {
            rideNowModel = this.rideDetailsService.ConfirmRide(rideNowModel);
            return rideNowModel;
        }

    }
}
