using System;
using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Repository
{
    public interface IUserService
    {
        User Add(User addedUser);
        bool EmailExists(string email);
        bool VerifyAccount(string id);
        bool IsEmailVerified(string email);
        User FindByEmail(string email);
        User FindByActivationCode(Guid activationCode);
        IList<User> FindAll();
        bool VerifyAccountAgain(string code);
        IList<User> FindAllAdmins();
    }
}