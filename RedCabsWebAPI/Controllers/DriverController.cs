﻿using System;
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
        ICarTypeService carTypeService;

        public DriverController(IDriverService driverService, ICarTypeService carTypeService)
        {
            this.driverService = driverService;
            this.carTypeService = carTypeService;
        }

        [Authorize]
        public List<Driver> Get()
        {
            var drivers = this.driverService.GetAll().ToList();

            return drivers;
        }

        [Authorize]
        public Driver Get(int id)
        {
            var driver = this.driverService.GetById(id);

            return driver;
        }

        [Authorize]
        public List<Driver> GetDriversByCarType(string carType)
        {
            var drivers = this.driverService.GetDriversByCarType(carType);
            return drivers;
        }    

    }
}
