using BusinessTripApplication.Models;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;

namespace BusinessTripApplication.Repository
{
    public class UserRepository : IUserRepository
    {

        public User Add(User addedUser)
        {
            addedUser.ActivationCode = Guid.NewGuid();
            addedUser.Password = Crypto.Hash(addedUser.Password);
            addedUser.IsEmailVerified = false;

            using (var dc = new UserContext())
            {
                dc.Users.Add(addedUser);
                return addedUser;

            }

            //return new User();
        }

        public bool EmailExists(string email)
        {
            using (var db = new UserContext())
            {
                var exists = db.Users.FirstOrDefault(a => a.Email == email);
                return exists != null;
            }
        }
    }
}