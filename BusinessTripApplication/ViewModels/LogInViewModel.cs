﻿using System;
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
    public class LogInViewModel : ILogInViewModel
    {
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

        public LogInViewModel(bool modelState, string email, string password,bool rememberMe, IUserService userService,out int returnValue)
        {
            if (modelState)
            {
                try
                {
                    User user=new User("",email,password);
                    Status = CheckUser(userService, user);
                    if (Status)
                    {
                        RememberMe = rememberMe;
                        Email = email;
                        Password = "";//do not expose password
                        SetCookie(email, rememberMe);
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
                    return;
                }
                catch (DatabaseException e)
                {
                    Message = e.Message;
                    returnValue = -1;
                    Status = false;
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
                    Message = " Sorry, you have to register first";
                    return false;
                }
                dbUser = userService.GetUserByEmail(user.Email);
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

            

            if (!emailVerified)
            {
                Message = " You have to verifiy your email first";
                return false;
            }

            if (!goodPassword)
            {
                Message = " Incorrect password";
                return false;
            }
            
            return true;
        }


        public void SetCookie(string email, bool rememberMe)
        {
            int timeout = rememberMe ? 525600 : 20; // 525600 min = 1 year
            var ticket = new FormsAuthenticationTicket(email, rememberMe, timeout);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            Cookie.Expires = DateTime.Now.AddMinutes(timeout);
            Cookie.HttpOnly = true;
        }

    }
}