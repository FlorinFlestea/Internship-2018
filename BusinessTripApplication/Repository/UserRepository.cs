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
                dc.SaveChanges();
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

        public bool VerifyAccount(string id)
        {
            using (var db = new UserContext())
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                var v = db.Users.FirstOrDefault(a => a.ActivationCode == new Guid(id));
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}