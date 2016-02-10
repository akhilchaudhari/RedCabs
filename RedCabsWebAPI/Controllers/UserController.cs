using Newtonsoft.Json;
using RideMe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Http;
using UnitOfWorkApplication.Model;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Model.Model;
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

        public User Post(User user)
        {
            try
            {
                var result = this.userService.CheckDuplicateEntryExists(user.ContactNo, user.Email);
                if(result.Count!=0)
                {
                    user = new User();
                    if(result.Contains("contact"))
                    {
                        user.IsDuplicateContact = true;
                    }
                    if (result.Contains("email"))
                    {
                        user.IsDuplicateEmail = true;
                    }
                    return user;
                }


                this.userService.AddUser(user);

                try
                {
                    EmailModel emailModel = new EmailModel();
                    emailModel.RecipientEmailAddress = user.Email;
                    emailModel.RecipientName = user.Name;

                    byte[] data = Convert.FromBase64String(user.Password);
                    emailModel.RecipientPassword = Encoding.UTF8.GetString(data);
                    emailModel.EmailSubject = String.Format(RideMeResources.Registration_Email_Subject, RideMeResources.CompanyName);
                    emailModel.EmailBody = String.Format(RideMeResources.Registration_Email_Body, emailModel.RecipientName, RideMeResources.CompanyName, RideMeResources.LowestCabRates, RideMeResources.CompanyName,
                                                    emailModel.RecipientEmailAddress, emailModel.RecipientPassword, RideMeResources.CompanyName, RideMeResources.CompanyName);

                    SendMail(emailModel);
                }
                catch
                {

                }               
            }
            catch
            {

            }
            return user;
        }          

        [HttpGet]
        public UserDetails AuthenticateUser(string json)
        {
            List<KeyValuePair> model = new List<KeyValuePair>();
            model = JsonConvert.DeserializeObject<List<KeyValuePair>>(json);
            var result = this.userService.AuthenticateUser(model);
            return result;

        }

        private void SendMail(EmailModel model)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(RideMeResources.SmtpClient);

            mail.From = new MailAddress(RideMeResources.CompanyRegistrationEmailAddress);
            mail.To.Add(model.RecipientEmailAddress);
            mail.Subject = model.EmailSubject;
            mail.Body = model.EmailBody;

            SmtpServer.Port = Int32.Parse(RideMeResources.EmailAddressPort);
            SmtpServer.Credentials = new System.Net.NetworkCredential(RideMeResources.CompanyRegistrationEmailAddress, RideMeResources.CompanyRegistrationEmailPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

    }
}
