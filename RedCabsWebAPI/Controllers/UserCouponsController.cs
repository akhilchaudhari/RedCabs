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
    public class UserCouponsController : ApiController
    {
        IUserCouponsService UserCouponsService;

        public UserCouponsController(IUserCouponsService UserCouponsService)
        {
            this.UserCouponsService = UserCouponsService;            
        }
        public List<UserCoupons> Get()
        {
            var UserCoupons = this.UserCouponsService.GetAll().ToList();
           
            return UserCoupons;
        }

        public List<UserCoupons> Get(int id)
        {
            var userCoupons = this.UserCouponsService.GetById(id);

            return userCoupons;
        }

        public List<UserCouponModel> GetActiveCouponsForUser(int userId)
        {
            var userCoupons = this.UserCouponsService.GetActiveCouponsForUser(userId);
            var userCouponDetails = userCoupons.Select(x => new UserCouponModel { CouponId = x.Coupon.Id, CouponCode = x.Coupon.CouponCode, Description = x.Coupon.Title }).ToList();
            return userCouponDetails;
        }
    }
}
