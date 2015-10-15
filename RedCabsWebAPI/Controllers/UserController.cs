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
        IPersonService personService;

        //public UserController()
        //{
        //}
        public UserController(IUserService userService, IPersonService personService)
        {
            this.userService = userService;
            this.personService = personService;
        }
        public List<User> Get()
        {
            var users = this.userService.GetAll().ToList();
            var persons = this.personService.GetAll().ToList();
           
            return users;
        }
    }
}
