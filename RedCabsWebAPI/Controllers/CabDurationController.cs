using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitOfWorkApplication.API;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Services.Interfaces;

namespace RedCabsWebAPI.Controllers
{
    public class CabDurationController : ApiController
    {
        IDriverService driverService;
        ICarTypeService carTypeService;

        public CabDurationController(IDriverService driverService, ICarTypeService carTypeService)
        {
            this.driverService = driverService;
            this.carTypeService = carTypeService;
        }

        public IEnumerable<CabDuration>  GetCabDurations(string latitude, string longitude, string apiKey)
        {
            DistanceMatrix distanceService = new DistanceMatrix();
            IEnumerable<CabDuration> cabDurations = distanceService.GetCabDurations(latitude, longitude, this.driverService.GetAllAvailableDrivers().ToList(), apiKey);
            return cabDurations;
        }

    }
}
