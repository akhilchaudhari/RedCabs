using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RedCabsWebAPI.Filters;
using RideMe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UnitOfWorkApplication.Model;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Services.Interfaces;
using UnitOfWorkApplication.Services.Services;

namespace RedCabsWebAPI.Controllers
{
    public class UserController : ApiController
    {
        private AuthRepository _repo = null;     

        IUserService userService;
        ICommunicationService communicationService;
        ILoggerService loggerService;

        public UserController(IUserService userService, ICommunicationService communicationService, ILoggerService loggerService)
        {
            this.userService = userService;
            this.communicationService = communicationService;
            this.loggerService = loggerService;
            _repo = new AuthRepository();
        }

        [Authorize]
        [HttpGet]
        public List<User> Get()
        {
            var users = this.userService.GetAll().ToList();

            return users;
        }

        [Authorize]
        [HttpGet]
        public User Get(int id)
        {
            var user = this.userService.GetById(id);

            return user;
        }

        public async Task<IHttpActionResult> Post(User user)
        {
            try
            {
                userService = null;              
                this.userService.AddUser(user);
                var abc = await _repo.RegisterUser(user);
                communicationService.SendMail(user);

                ApplicationUser userPSK = await _repo.FindUser(user.Email, user.Password);

                return Ok(new { PSK = userPSK.PSK });
            }
            catch (Exception ex)
            {
                loggerService.Log(new ExceptionLog()
                {
                    MethodName = "Post",
                    ClassName = this.GetType().Name,
                    ErrorMessage = ex.Message,
                    ExceptionType = ex.GetType().ToString(),
                    IsServerException = true,
                    ObjectDetails = Newtonsoft.Json.JsonConvert.SerializeObject(user),
                    StackTrace = ex.StackTrace,
                    Tag = "User Registration"
                });
                return BadRequest();
            }          
        }

        [HttpGet]
        public int CheckIfContactExists(string contactNo)
        {
            return this.userService.CheckIfContactExists(contactNo);
        }

        [HttpGet]
        public int CheckIfEmailExists(string email)
        {
            return this.userService.CheckIfEmailExists(email);
        }

        [TwoFactorAuthorize]
        [HttpPost]
        public IHttpActionResult CheckVerificationCode()
        {
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }
        //[HttpGet]
        //public UserDetails AuthenticateUser(string json)
        //{
        //    List<KeyValuePair> model = new List<KeyValuePair>();
        //    model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);
        //    var result = this.userService.AuthenticateUser(model);
        //    return result;

        //}
    }
}
