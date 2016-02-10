using RideMe.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model;

namespace UnitOfWorkApplication.Services.Services
{
    public class EmailService
    {
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
