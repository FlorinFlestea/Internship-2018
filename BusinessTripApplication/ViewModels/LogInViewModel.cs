using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.ModelBinding;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.ViewModels
{
    public class LogInViewModel
    {
        public string Email;
        public string Password;

        public string Message { get; }

        public bool Status { get; }

        public string Title { get; }

        public LogInViewModel()
        {
            Message = null;
            Title = "LogIn";
        }

        public LogInViewModel(bool modelState, string Email, string Password, IUserService userService)
        {
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

                //TODO: LOGIN REDIRECT
            }
            else
            {
                Message = "Invalid request";
                Status = false;
            }
        }
        

    }
}