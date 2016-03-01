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
    public class CarTypeController : ApiController
    {
        ICarTypeService carTypeService;

        public CarTypeController(ICarTypeService carTypeService)
        {
            this.carTypeService = carTypeService;            
        }

        [Authorize]
        public List<CarType> Get()
        {
            var carTypes = this.carTypeService.GetAll().ToList();
           
            return carTypes;
        }

        [Authorize]
        public CarType Get(int id)
        {
            var carType = this.carTypeService.GetById(id);

            return carType;
        }
    }
}
