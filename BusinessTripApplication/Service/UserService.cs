using BusinessTripApplication.Models;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;

namespace BusinessTripApplication.Repository
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            try
            {
                this.userRepository = userRepository;
            }
            catch
            {
                throw;
            }
            
        }

        public User Add(User addedUser)
        {
            try
            {
                return userRepository.Add(addedUser);
            }
            catch
            {
                throw;
            }
            
        }

        public bool EmailExists(string email)
        {
            try
            {
                return userRepository.FindByEmail(email) != null;
            }
            catch
            {
                throw;
            }
            
        }

        public IList<User> FindAll()
        {
            try
            {
                return userRepository.FindAll();
            }
            catch
            {
                throw;
            }
            
        }

        public bool IsEmailVerified(string email)
        {
            try
            {
                return userRepository.FindByEmail(email).IsEmailVerified == true;
            }
            catch
            {
                throw;
            }

        }

        public User FindByEmail(string email)
        {
            return userRepository.FindByEmail(email);
        }

        public bool VerifyAccount(string id)
        {
            User user;
            try
            {
                user = userRepository.FindByActivationCode(new Guid(id));
            }
            catch
            {
                throw;
            }
            

            if (user != null && !user.IsEmailVerified)
            {
                user.IsEmailVerified = true;
                try
                {
                    userRepository.UpdateIsEmailVerified(user);
                }
                catch
                {
                    throw;
                }
                
                return true;
            }

            return false;
        }
    }
}
