using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using BusinessTripModels.Exception;
using BusinessTripModels.Models;
using BusinessTripApplication.Models;

namespace BusinessTripApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private static readonly log4net.ILog Logger
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public User Add(User addedUser)
        {
            addedUser.ActivationCode = Guid.NewGuid();
            addedUser.Password = Crypto.Hash(addedUser.Password);
            addedUser.IsEmailVerified = false;

            try
            {
                using (var context = new DatabaseContext())
                {
                    addedUser.Role = context.Roles.FirstOrDefault(t => t.Type == "User");

                    context.Users.Add(addedUser);
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to Database!\n");
            }

            return addedUser;
        }

        public IList<User> FindAll()
        {
            IList<User> users = new List<User>();
            try
            {
                using (var context = new DatabaseContext())
                {
                    users = context.Users.ToList();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to Database!\n");
            }

            return users;
        }

        public User FindByActivationCode(Guid code)
        {
            IList<User> users = FindAll();

            return users.SingleOrDefault(x => x.ActivationCode == code);
        }

        public User FindByEmail(string email)
        {
            IList<User> users = FindAll();

            return users.SingleOrDefault(x => x.Email == email);
        }

        public User UpdateIsEmailVerified(User updatedUser)
        {
            User update;
            try
            {
                using (var context = new DatabaseContext())
                {
                    update = context.Users.SingleOrDefault(user => user.Id == updatedUser.Id);
                    update.IsEmailVerified = updatedUser.IsEmailVerified;
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to Database!\n");
            }

            return update;
        }
    }
}