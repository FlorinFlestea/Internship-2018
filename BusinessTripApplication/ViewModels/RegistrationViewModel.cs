using System.Net;
using System.Net.Mail;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.ViewModels
{
    public class RegistrationViewModel 
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

        public RegistrationViewModel(bool modelState, User user, IUserRepository UserRepository)
        {
            if (modelState)
            {
                var emailExists = UserRepository.EmailExists(user.Email);
                if (emailExists)
                {
                    Message = "Email already in the database !";
                    Status = false;
                    return;
                }

                User = UserRepository.Add(user);

                //Send Email to User
                SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                Message = "Registration successfully done. Account activation link " +
                                  " has been sent to your email id:" + user.Email;
                Status = true;
            }
            else
            {
                Message = "Invalid request";
                Status = false;
            }
        }
        public void SendVerificationLinkEmail(string emailId, string activationCode)
        {

            string domainName = "http://localhost:54301";
        

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
                smtp.Send(message);
        }

    }
}
