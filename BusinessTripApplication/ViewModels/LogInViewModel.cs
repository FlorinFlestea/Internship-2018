using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using BusinessTripApplication.Repository;
using BusinessTripModels.Models;
using BusinessTripModels.Exception;

namespace BusinessTripApplication.ViewModels
{
    public class LogInViewModel : ILogInViewModel
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string Email;
        public string Password;
        public bool RememberMe;

        public HttpCookie Cookie;

        public string Message { get; set; }

        public bool Status { get; }

        public string Title { get; }

        public LogInViewModel()
        {
            Message = null;
            Title = "LogIn";
        }


        public LogInViewModel(IUserService userService, Guid activationCode)
        {
            SetCookie(userService.FindByActivationCode(activationCode).Email, true);
        }

        public LogInViewModel(bool modelState, string email, string password,bool rememberMe, IUserService userService,out int returnValue)
        {
            if (modelState)
            {
                try
                {
                    User user = new User("", email, password);
                    RememberMe = rememberMe;
                    Status = CheckUser(userService, user);
                    if (Status)
                    {
                        Email = email;
                        Password = "";//do not expose password

                        returnValue = 1;
                        return;
                    }
                    returnValue = -1;
                    return;
                }
                catch (InternetException e)
                {
                    Message = e.Message;
                    Status = false;
                    returnValue = -1;
                    Logger.Info(e.Message);
                    return;
                }
                catch (DatabaseException e)
                {
                    Message = e.Message;
                    returnValue = -1;
                    Status = false;
                    Logger.Info(e.Message);
                    return;
                }
            }
            returnValue = -1;
            Message = " Invalid request";
            Status = false;
        }

        public bool CheckUser(IUserService userService, User user)
        {
            User dbUser;
            bool emailExists;
            try
            {
                emailExists = userService.EmailExists(user.Email);
                if (!emailExists)
                {
                    Message = " Your email is invalid or your password is invalid or" +
                              " you haven't verified your email!";
                    return false;
                }
                dbUser = userService.FindByEmail(user.Email);
            }
            catch
            {
                throw;
            }

            bool emailVerified;
            try
            {
                emailVerified = userService.IsEmailVerified(dbUser.Email);
            }
            catch
            {
                throw;
            }

            bool goodPassword;
            try
            {
                if (dbUser.Password == Crypto.Hash(user.Password))
                    goodPassword = true;
                else
                    goodPassword = false;
            }
            catch
            {
                throw;
            }

            if (!goodPassword || !emailVerified)
            {
                Message = " Your email is invalid or your password is invalid or" +
                          " you haven't verified your email!";
                return false;
            }
            SetCookie(user.Email, RememberMe);
            return true;
        }


        public void SetCookie(string email, bool rememberMe)
        {
            int timeout = rememberMe ? 262800 : 20; // 262800 min = 1/2 year
            var ticket = new FormsAuthenticationTicket(email, rememberMe, timeout);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            Cookie.Expires = DateTime.Now.AddMinutes(timeout);
            Cookie.HttpOnly = true;
        }

    }
}