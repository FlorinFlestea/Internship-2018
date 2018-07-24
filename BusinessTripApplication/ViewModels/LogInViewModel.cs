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

        public HttpCookie cookie;

        public string Message { get; }

        public bool Status { get; }

        public string Title { get; }

        public LogInViewModel()
        {
            Message = null;
            Title = "LogIn";
        }

        public LogInViewModel(bool modelState, string Email, string Password,bool RememberMe, IUserService userService,out int returnValue)
        {
            var message = "";
            using (UserContext dc = new UserContext())
            {
                var v = dc.Users.Where(a => a.Email == Email).FirstOrDefault();
                if (v != null)
                {
                    if (!v.IsEmailVerified)
                    {
                        Message = "Please verify your email first";

                        returnValue = -1;
                        return;
                    }

                    if (string.Compare(Crypto.Hash(Password), v.Password) == 0)
                    {
                        int timeout = RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(Email, RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        
                        returnValue = 1;
                        return;
                        /*
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            returnValue = 1;
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        */
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            Message = message;
            returnValue = -1;
            return;
            /*
            //TODO: LOGIN COOKIES

            if (modelState)
            {
                var emailExists = userService.EmailExists(Email);
                if (!emailExists)
                {
                    Message = "No such email !";
                    Status = false;
                    return;
                }
                //if(userService.)

                //TODO: LOGIN REDIRECT
            }
            else
            {
                Message = "Invalid request";
                Status = false;
            }
            */

        }



    }
}