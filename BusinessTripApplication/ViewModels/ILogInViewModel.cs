using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.ViewModels;

namespace BusinessTripApplication.ViewModels
{
    public interface ILogInViewModel
    {
        bool CheckUser(IUserService userService, User user);
        void SetCookie(string email, bool rememberMe);
    }
}