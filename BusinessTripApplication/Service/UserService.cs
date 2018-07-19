using BusinessTripApplication.Models;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;

namespace BusinessTripApplication.Repository
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Add(User addedUser)
        {
            return userRepository.Add(addedUser);
        }

        public bool EmailExists(string email)
        {
            return userRepository.FindByEmail(email) != null;
        }

        public bool VerifyAccount(string id)
        {
            User user = userRepository.FindByActivationCode(new Guid(id));

            if (user != null)
            {
                user.IsEmailVerified = true;
                userRepository.Update(user);
                return true;
            }

            return false;
        }
    }
}