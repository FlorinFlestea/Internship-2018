using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.ModelBinding;
using System.Web.Security;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.ViewModels
{
    public class LogInViewModel
    {
        public string Email;
        public string Password;
        public bool RememberMe;

        public HttpCookie Cookie;

        public string Message { get; }

        public bool Status { get; }

        public string Title { get; }

        public LogInViewModel()
        {
            Message = null;
            Title = "LogIn";
        }

        public LogInViewModel(bool modelState, string email, string password,bool rememberMe, IUserService userService,out int returnValue)
        {
            if (modelState)
            {
                var message = "";
                using (DatabaseContext dc = new DatabaseContext())
                {
                    var v = dc.Users.Where(a => a.Email == email).FirstOrDefault();
                    if (v != null)
                    {
                        if (!v.IsEmailVerified)
                        {
                            Message = "Please verify your email first";
                            returnValue = -1;
                            return;
                        }

                        if (string.Compare(Crypto.Hash(password), v.Password) == 0)
                        {
                            int timeout = rememberMe ? 525600 : 20; // 525600 min = 1 year
                            var ticket = new FormsAuthenticationTicket(email, rememberMe, timeout);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            Cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            Cookie.HttpOnly = true;


                            RememberMe = rememberMe;
                            Email = email;
                            Password = "";//do not expose password

                            returnValue = 1;
                            return;
                        }
                        else
                        {
                            message = "Invalid credentials provided";
                        }
                    }
                    else
                    {
                        message = "Invalid credentials provided";
                    }
                }
                Message = message;
                returnValue = -1;
                return;
            }
            else
            {
                returnValue = -1;
                Message = "Invalid request";
                Status = false;
            }
        }
    }
}