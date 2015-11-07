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
    public class UserController : ApiController
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public List<User> Get()
        {
            var users = this.userService.GetAll().ToList();

            return users;
        }

        public User Get(int id)
        {
            var user = this.userService.GetById(id);

            return user;
        }

        public void Add(User user)
        {
            this.userService.Add(user);
        }    

    }
}
