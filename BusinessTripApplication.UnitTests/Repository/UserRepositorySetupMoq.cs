using BusinessTripApplication.Repository;
using System;
using System.Linq;
using Moq;
using BusinessTripModels.Models;
using System.Collections.Generic;

namespace BusinessTripApplication.UnitTests.Repository
{
    public static class UserRepositorySetupMoq
    {
        public static void FindByEmail(Mock<IUserRepository> MockUserRepository, IList<User> users)
        {
            MockUserRepository.Setup(mr => mr.FindByEmail(It.IsAny<string>())).Returns(
                (string email) => users.SingleOrDefault(x => x.Email == email));
        }

        public static void FindByActivationCode(Mock<IUserRepository> MockUserRepository, IList<User> users)
        {
            MockUserRepository.Setup(mr => mr.FindByActivationCode(It.IsAny<Guid>())).Returns(
                (Guid id) => users.SingleOrDefault(x => x.ActivationCode == id));
        }

        public static void FindAll(Mock<IUserRepository> MockUserRepository, IList<User> users)
        {
            MockUserRepository.Setup(mr => mr.FindAll()).Returns(
                users);
        }

        public static void UpdateIsEmailVerified(Mock<IUserRepository> MockUserRepository, IList<User> users)
        {
            MockUserRepository.Setup(mr => mr.UpdateIsEmailVerified(It.IsAny<User>())).Returns(
                (User update) =>
                {
                    User user = users.SingleOrDefault(x => x.Id == update.Id);

                    if (user != null)
                    {
                        user.IsEmailVerified = update.IsEmailVerified;
                    }
                    return user;
                });
        }

        public static void Add(Mock<IUserRepository> MockUserRepository, IList<User> users)
        {
            MockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Returns(
                (User addedUser) =>
                {
                    users.Add(addedUser);
                    return addedUser;
                });
        }
    }
}
