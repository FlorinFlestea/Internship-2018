using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.ViewModels
{
    public class RegistrationViewModel : IRegistrationViewModel
    {
        public User User;

        public string Message { get; }

        public bool Status { get; }

        public string Title { get; }

        public RegistrationViewModel()
        {
            Message = null;
            Title = "Registration";

        }

        public RegistrationViewModel(bool modelState, User user, IUserService userService)
        {
            if (modelState)
            {
                Status = CheckUser(userService, user);
                if(!Status)
                    Message = "Email already in the database !";
                else
                    Message = "Registration successfully done. Account activation link " +
                                  " has been sent to your email id:" + user.Email;               
            }
            else
            {
                Message = "Invalid request";
                Status = false;
            }
        }

        public bool CheckUser(IUserService userService, User user)
        {
            var emailExists = userService.EmailExists(user.Email);
            if (emailExists)
            {
                return false;
            }

            User = userService.Add(user);
            User.Password = "";

            //Send Email to User
            SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());

            return true;
        }
        public void SendVerificationLinkEmail(string emailId, string activationCode)
        {
            string domainName = "https://localhost:44328";

            var link = domainName + "/User/VerifyAccount/" + activationCode;

            var fromEmail = new MailAddress("businesstripapplication@gmail.com", "Registration");
            var toEmail = new MailAddress(emailId);
            var fromEmailPassword = "ParolaTest1234"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/>We are excited to tell you that your account is" +
                            " successfully created. Please click on the below link to verify your account" +
                            " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}