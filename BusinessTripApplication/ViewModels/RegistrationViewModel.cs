using System.Net;
using System.Net.Mail;
using BusinessTripModels.Exception;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Server;
using BusinessTripModels.Models;


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
                Server.EmailSender emailSender = new EmailSender();
                string url = "https://localhost:44328/User/VerifyAccount/" + user.ActivationCode.ToString();
                string message = "We are excited to tell you that your account is successfully created. " +
                                 "Please <a href='" + url + "'>Click here </a> to verify your account. </br>";

                emailSender.SendEmail(user.Email, "Register", message);
            }
            catch
            {
                throw;
            }


            return true;
        }


    }
}