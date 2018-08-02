using System.Net;
using System.Net.Mail;
using BusinessTripApplication.Exception;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripModels;


namespace BusinessTripApplication.ViewModels
{
    public class RegistrationViewModel : IRegistrationViewModel
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                try
                {
                    Status = CheckUser(userService, user);
                }
                catch (InternetException e)
                {
                    Message = e.Message;
                    Status = false;
                    return;
                }
                catch (DatabaseException e)
                {
                    Message = e.Message;
                    Status = false;
                    return;
                }

                Message = " Registration successfully done. Account activation link " +
                                  " has been sent to your email id:" + user.Email;
                Status = true;
            }
            else
            {
                Message = " Invalid request";
                Status = false;
            }
        }

        public bool CheckUser(IUserService userService, User user)
        {
            bool emailExists;
            try
            {
                emailExists = userService.EmailExists(user.Email);
            }
            catch
            {
                throw;
            }

            if (emailExists)
            {
                throw new DatabaseException(" Email already exists!\n");
            }
            try
            {
                User = userService.Add(user);
            }
            catch
            {
                throw;
            }

            User.Password = "";

            //Send Email to User
            try
            {
                SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
            }
            catch
            {
                throw;
            }


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
                catch (System.Exception ex)
                {
                    Logger.Info(ex.Message);
                    throw new InternetException("Cannot connect to internet!\n");
                }
        }

    }
}