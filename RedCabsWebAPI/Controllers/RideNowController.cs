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

        public RideNowModel GetRideEstimates(string serializableModel)
        {
            RideNowModel model = JsonConvert.DeserializeObject<RideNowModel>(serializableModel);
            this.userService.GetFareEstimate(model);
            return model;
        }
    }
}
