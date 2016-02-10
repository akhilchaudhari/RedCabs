using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitOfWorkApplication.API;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Services.Interfaces;

namespace RedCabsWebAPI.Controllers
{
    public class RideNowController : ApiController
    {
        IDriverService driverService;
        ICarTypeService carTypeService;
        IUserService userService;        

        public RideNowController(IDriverService driverService, ICarTypeService carTypeService, IUserService userService)
        {
            this.driverService = driverService;
            this.carTypeService = carTypeService;
            this.userService = userService;
        }      

        public RideNowModel GetRideEstimates(string json)
        {
            List<KeyValuePair> model = new List<KeyValuePair>();
            model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);
            RideNowModel rideNowModel = JsonConvert.DeserializeObject<RideNowModel>(model[0].Value);
            rideNowModel = this.userService.GetFareEstimate(rideNowModel);
            return rideNowModel;
        }

        //public DriverDetails ConfirmRide(string json)
        //{
        //    List<KeyValuePair> model = new List<KeyValuePair>();
        //    model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);
        //    RideNowModel rideNowModel = JsonConvert.DeserializeObject<RideNowModel>(model[0].Value);
        //    rideNowModel = this.userService.GetFareEstimate(rideNowModel);
        //    return rideNowModel.DriverDetails;
        //}

    }
}
