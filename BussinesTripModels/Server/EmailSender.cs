using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using log4net.Repository.Hierarchy;
using BusinessTripModels.Exception;


namespace BusinessTripApplication.Server
{
    public class EmailSender : IEmailSender
    {
        private readonly MailAddress fromEmail = new MailAddress("businesstripapplication@gmail.com");
        private readonly string fromEmailPassword = "ParolaTest1234";

        private readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SmtpClient CreateSmtpClient()
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            return smtp;
        }

        public MailMessage CreateMail(string email, string subject, string message)
        {
            var mail = new MailMessage(fromEmail, new MailAddress(email))
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            return mail;
        }

        public void SendEmail(string email, string subject, string message)
        {
            SmtpClient smtp = CreateSmtpClient();
            MailMessage mail = CreateMail(email, subject, message);

            try
            {
                smtp.Send(mail);
            }
            catch (System.Exception ex)
            {
                Logger.Info(ex.Message);
                throw new InternetException("Cannot connect to internet!\n");
            }
        }
    }
}