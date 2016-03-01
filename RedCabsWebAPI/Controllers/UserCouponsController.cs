using Newtonsoft.Json;
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
    public class UserCouponsController : ApiController
    {
        IUserCouponsService UserCouponsService;

        public UserCouponsController(IUserCouponsService UserCouponsService)
        {
            this.UserCouponsService = UserCouponsService;            
        }

        [Authorize]
        public List<UserCoupons> Get()
        {
            var UserCoupons = this.UserCouponsService.GetAll().ToList();
           
            return UserCoupons;
        }

        [Authorize]
        public List<UserCoupons> Get(int id)
        {
            var userCoupons = this.UserCouponsService.GetById(id);

            return userCoupons;
        }

        [Authorize]
        public List<Coupon> GetActiveCouponsForUser(string json)
        {

            List<KeyValuePair> model = new List<KeyValuePair>();
            model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);
            int userId = Int32.Parse(model.Where(x => x.Key.Equals("userid",StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value);

            var userCoupons = this.UserCouponsService.GetActiveCouponsForUser(userId);
            var userCouponDetails = userCoupons.Select(x => x.Coupon).ToList();
            return userCouponDetails;
        }
    }
}
