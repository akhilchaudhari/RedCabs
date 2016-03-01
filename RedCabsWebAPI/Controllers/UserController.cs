using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedCabsWebAPI.Filters;
using RideMe.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UnitOfWorkApplication.API.Services;
using UnitOfWorkApplication.Model;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Enum;
using UnitOfWorkApplication.Model.Model;
using UnitOfWorkApplication.Services.Interfaces;
using UnitOfWorkApplication.Services.Services;

namespace RedCabsWebAPI.Controllers
{
    public class UserController : ApiController
    {
        private AuthRepository _repo = null;
        //private const string BaseURL = "http://localhost:6015";
        private const string BaseURL = "https://ridemecabs.azurewebsites.net";

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
        public IEnumerable<User> Get()
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

        [HttpPost]
        public async Task<IHttpActionResult> Post(User user)
        {
            try
            {
                this.userService.AddUser(user);
                var abc = await _repo.RegisterUser(user);
                communicationService.SendMail(user);                                

                ApplicationUser userPSK = await _repo.FindUser(user.Email, user.Password);

                var accessToken = GetToken(user.Email, user.Password);

                int otp = TimeSensitivePassCode.GetOTP(userPSK.PSK);

                if (otp.ToString().Length == 6)
                {
                    communicationService.SendMail(user.Email, user.Name, otp.ToString());
                }

                if (string.IsNullOrEmpty(accessToken) || user.Id==0)
                {
                    return BadRequest();
                }

                return Ok(new { AccessToken = accessToken, Id = user.Id});
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
        public DuplicateEntry CheckIfContactExists(string json)
        {
            List<KeyValuePair> model = new List<KeyValuePair>();
            model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);
            return this.userService.CheckIfContactExists(model.First().Value);
        }

        [HttpGet]
        public DuplicateEntry CheckIfEmailExists(string json)
        {
            List<KeyValuePair> model = new List<KeyValuePair>();
            model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);
            return this.userService.CheckIfEmailExists(model.First().Value);
        }

        [TwoFactorAuthorize]
        [HttpPost]
        public IHttpActionResult VerifyOTP(string json)
        {
            try
            {
                KeyValuePair model = new KeyValuePair();                
                var userDetails = this.userService.GetById(Int32.Parse(json));
                userDetails.ContactVerificationStatus = 2;
                this.userService.Update(userDetails);
                return Ok(new { Status = "Verified" });
            }
            catch(Exception ex)
            {
                loggerService.Log(new ExceptionLog()
                {
                    MethodName = "VerifyOTP",
                    ClassName = this.GetType().Name,
                    ErrorMessage = ex.Message,
                    ExceptionType = ex.GetType().ToString(),
                    IsServerException = true,
                    ObjectDetails =json,
                    StackTrace = ex.StackTrace,
                    Tag = "OTP Verification"
                });
                return Ok(new { Status = "Error" });
            }
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        [HttpGet]
        public string GetToken(string username, string password)
        {            
            string accessToken=String.Empty;
            var request = WebRequest.Create("https://ridemecabs.azurewebsites.net"+"/token") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            var authCredentials = "grant_type=password&userName=" + username + "&password=" + password;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(authCredentials);
            request.ContentLength = bytes.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                var responseFromServer = reader.ReadToEnd();
                accessToken = JObject.Parse(responseFromServer).SelectToken("$.access_token").ToString();
                var authCookie = response.Cookies["access_token"];
            }
            return accessToken;
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> GetOTP(int userId)
        {
            var user = this.userService.GetById(userId);
            ApplicationUser userPSK = await _repo.FindUser(user.Email, user.Password);

            int otp = TimeSensitivePassCode.GetOTP(userPSK.PSK);

            if (otp.ToString().Length!=6)
            {
                return BadRequest();
            }
            return Ok(new { OTP = otp});
        }

        [HttpGet]
        public RideNowModel AuthenticateUser(string json)
        {
            List<KeyValuePair> model = new List<KeyValuePair>();
            model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);            
            RideNowModel result = this.userService.AuthenticateUser(model[0].Value,model[1].Value);

            result.AccessToken=  GetToken(result.userDetails.Email, result.userDetails.Password);

            return result;

        }

    }
}
