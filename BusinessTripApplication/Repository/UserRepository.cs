using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using BusinessTripApplication.Models;

namespace BusinessTripApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Add(User addedUser)
        {
            addedUser.ActivationCode = Guid.NewGuid();
            addedUser.Password = Crypto.Hash(addedUser.Password);
            addedUser.IsEmailVerified = false;

            using (var context = new BusinessContext())
            {
                context.Users.Add(addedUser);
                context.SaveChanges();

            }

            return addedUser;
        }

        public IList<User> FindAll()
        {
            IList<User> users = new List<User>();

            using (var context = new BusinessContext())
            {
                users = context.Users.ToList();
            }

            return users;
        }

        public User FindByActivationCode(Guid code)
        {
            IList<User> users = FindAll();

            return users.Where(x => x.ActivationCode == code).SingleOrDefault();
        }

        public User FindByEmail(string email)
        {
            IList<User> users = FindAll();

            return users.Where(x => x.Email == email).SingleOrDefault();
        }

        public User UpdateIsEmailVerified(User updatedUser)
        {
            User update;
            using (var context = new BusinessContext())
            {
                update = context.Users.SingleOrDefault(user => user.Id == updatedUser.Id);
                update.IsEmailVerified = updatedUser.IsEmailVerified;
                context.SaveChanges();
            }
            return update;
        }
    }
}