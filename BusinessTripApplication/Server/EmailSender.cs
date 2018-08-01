using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using log4net.Repository.Hierarchy;


namespace BusinessTripApplication.Server
{
    public static class EmailSender
    {
        private static readonly MailAddress fromEmail = new MailAddress("businesstripapplication@gmail.com");
        private static readonly string fromEmailPassword = "ParolaTest1234";

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static SmtpClient CreateSmtpClient()
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

        public static MailMessage CreateMail(string email, string subject, string message)
        {
            var mail = new MailMessage(fromEmail, new MailAddress(email))
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            return mail;
        }

        public static void SendEmail(string email, string subject, string message)
        {
            SmtpClient smtp = CreateSmtpClient();
            MailMessage mail = CreateMail(email, subject, message);

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Logger.Info(ex.Message);
                throw new InternetException("Cannot connect to internet!\n");
            }
        }
    }
}