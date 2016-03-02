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
    public class CabDurationController : ApiController
    {
        IDriverService driverService;
        ICarTypeService carTypeService;

        public CabDurationController(IDriverService driverService, ICarTypeService carTypeService)
        {
            this.driverService = driverService;
            this.carTypeService = carTypeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
       // [Authorize]
        public IEnumerable<CabDuration>  GetCabDurations(string json)
        {
            List<KeyValuePair> model = new List<KeyValuePair>();
            model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);

            DistanceMatrix distanceService = new DistanceMatrix();
            IEnumerable<CabDuration> cabDurations = distanceService.GetCabDurations(model.Where(x=>x.Key.Equals("latitude",StringComparison.OrdinalIgnoreCase)).First().Value,
                                                                                    model.Where(x => x.Key.Equals("longitude", StringComparison.OrdinalIgnoreCase)).First().Value,
                                                                                    Int32.Parse(model.Where(x => x.Key.Equals("driverId", StringComparison.OrdinalIgnoreCase)).First().Value),
                                                                                    this.driverService.GetAllActiveDrivers().ToList());
            return cabDurations;
        }

    }
}
