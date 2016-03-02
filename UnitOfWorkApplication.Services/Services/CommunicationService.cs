using RideMe.Infrastructure;
using System;
using System.Collections.Generic;
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
                
                model.EmailSubject = String.Format(RideMeResources.Registration_Email_Subject, RideMeResources.CompanyName);
                model.EmailBody = String.Format(RideMeResources.Registration_Email_Body, model.RecipientName, RideMeResources.CompanyName, RideMeResources.LowestCabRates, RideMeResources.CompanyName,
                                                model.RecipientEmailAddress, RideMeResources.CompanyName, RideMeResources.CompanyName);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(RideMeResources.SmtpClient);

                mail.From = new MailAddress(RideMeResources.CompanyRegistrationEmailAddress);
                mail.To.Add(model.RecipientEmailAddress);
                mail.Subject = model.EmailSubject;
                mail.Body = model.EmailBody;

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Port = Int32.Parse(RideMeResources.EmailAddressPort);
                SmtpServer.Credentials = new System.Net.NetworkCredential(RideMeResources.CompanyRegistrationEmailAddress, RideMeResources.CompanyRegistrationEmailPassword);

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
              System.Security.Cryptography.X509Certificates.X509Certificate certificate,
              System.Security.Cryptography.X509Certificates.X509Chain chain,
              System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

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

        public void SendMail(string email,string name, string otp)
        {
            try
            {
                EmailModel model = new EmailModel();
                model.RecipientEmailAddress = email;
                model.RecipientName = name;

                model.EmailSubject = String.Format(RideMeResources.CompanyName, RideMeResources.OTPEmailSubject);
                model.EmailBody = String.Format(RideMeResources.OTP_Email_Body, model.RecipientName, RideMeResources.CompanyName, otp);

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(RideMeResources.SmtpClient);

                mail.From = new MailAddress(RideMeResources.CompanyRegistrationEmailAddress);
                mail.To.Add(model.RecipientEmailAddress);
                mail.Subject = model.EmailSubject;
                mail.Body = model.EmailBody;

                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Port = Int32.Parse(RideMeResources.EmailAddressPort);
                SmtpServer.Credentials = new System.Net.NetworkCredential(RideMeResources.CompanyRegistrationEmailAddress, RideMeResources.CompanyRegistrationEmailPassword);            

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
              System.Security.Cryptography.X509Certificates.X509Certificate certificate,
              System.Security.Cryptography.X509Certificates.X509Chain chain,
              System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                loggerService.Log(new ExceptionLog()
                {
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name,
                    ClassName = this.GetType().Name,
                    ErrorMessage = ex.Message,
                    ExceptionType = ex.GetType().ToString(),
                    IsServerException = true,
                    ObjectDetails = Newtonsoft.Json.JsonConvert.SerializeObject(new List<Object>() {email,name,otp }),
                    StackTrace = ex.StackTrace,
                    Tag = "Send OTP"
                });
            }
        }
    }
}
