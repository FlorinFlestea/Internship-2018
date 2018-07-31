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

        private static MailMessage CreateMail(MailAddress toEmail, string subject, string body)
        {
            using (var mail = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                return mail;
        }

        private static SmtpClient CreateSmtpClient()
        {
            SmtpClient smtp = new SmtpClient
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

        public static void SendEmail(string email, string subject, string message)
        {
            MailMessage mail = CreateMail(new MailAddress(email), subject, message);
            SmtpClient smtp = CreateSmtpClient();

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