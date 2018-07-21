using BusinessTripApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTripApplication.Repository
{
    public interface IUserRepository
    {
        User Add(User addedUser);
        User FindByEmail(string email);
        User FindByActivationCode(Guid code);
        User UpdateIsEmailVerified(User updatedUser);
        IList<User> FindAll();
    }
}