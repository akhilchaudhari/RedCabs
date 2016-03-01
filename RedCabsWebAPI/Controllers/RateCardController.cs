using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Services.Interfaces;

namespace RedCabsWebAPI.Controllers
{
    public class RateCardController : ApiController
    {
        IRateCardService rateCardService;

        public RateCardController(IRateCardService rateCardService)
        {
            this.rateCardService = rateCardService;            
        }

        [Authorize]
        public List<RateCardModel> Get()
        {
            var rateCard = this.rateCardService.GetAll().ToList();

            var rateCardModel = rateCard.Select(x => new RateCardModel { CarType = x.CarType.Type, DisplayPrice = x.DisplayPrice, DistanceDisplayText = x.DistanceDisplayText, IsAirportDrop = x.IsAirportDrop, IsPerKmPrice = x.IsPerKmPrice }).ToList();
           
            return rateCardModel;
        }       
    }
}
