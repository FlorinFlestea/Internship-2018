﻿using BusinessTripApplication.Models;
using System.Collections.Generic;

namespace BusinessTripApplication.Repository
{
    public interface IUserService
    {
        User Add(User addedUser);
        bool EmailExists(string email);
        bool VerifyAccount(string id);
        IList<User> FindAll();
    }
}