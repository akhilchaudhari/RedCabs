using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Services.Interfaces;

namespace RedCabsWebAPI.Controllers
{
    public class DriverController : ApiController
    {
        IDriverService driverService;

        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;            
        }
        public List<Driver> Get()
        {
            var drivers = this.driverService.GetAll().ToList();
           
            return drivers;
        }

        public Driver Get(int id)
        {
            var driver = this.driverService.GetById(id);

            return driver;
        }

        public List<Driver> GetDriversByCarType(string carType);

    }
}
