using RideMe.Infrastructure;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Services.Interfaces;

namespace UnitOfWorkApplication.Services.Services
{
    public class CommunicationService : ICommunicationService
    {
        ILoggerService loggerService;

        public CommunicationService(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }


        public void SendMail(User user)
        {
            try
            {
                EmailModel model = new EmailModel();
                model.RecipientEmailAddress = user.Email;
                model.RecipientName = user.Name;

                byte[] data = Convert.FromBase64String(user.Password);
                model.RecipientPassword = Encoding.UTF8.GetString(data);
                model.EmailSubject = String.Format(RideMeResources.Registration_Email_Subject, RideMeResources.CompanyName);
                model.EmailBody = String.Format(RideMeResources.Registration_Email_Body, model.RecipientName, RideMeResources.CompanyName, RideMeResources.LowestCabRates, RideMeResources.CompanyName,
                                                model.RecipientEmailAddress, model.RecipientPassword, RideMeResources.CompanyName, RideMeResources.CompanyName);

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
            catch(Exception ex)
            {  
                loggerService.Log(new ExceptionLog()
                {
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name,
                    ClassName = this.GetType().Name,
                    ErrorMessage = ex.Message,
                    ExceptionType = ex.GetType().ToString(),
                    IsServerException = true,
                    ObjectDetails = Newtonsoft.Json.JsonConvert.SerializeObject(user),
                    StackTrace = ex.StackTrace,
                    Tag = "User Registration"
                });
            }
        }
    }
}
