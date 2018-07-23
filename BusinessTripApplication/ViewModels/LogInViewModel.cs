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
    public class LogInViewModel
    {
        public User User;

        public string Message { get; }

        public bool Status { get; }

        public string Title { get; }

        public LogInViewModel()
        {
            Message = null;
            Title = "LogIn";
        }

        public LogInViewModel(bool modelState, User user, IUserService userService)
        {
            //TODO: LOGIN LOGIC
        }
        

    }
}