using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.UnitTests.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessTripApplication.UnitTests.Service
{
    [TestClass]
    public class UserServiceTests
    {
        public readonly IUserService userService;

        public UserServiceTests()
        {
            UserRepositoryTests mockUserRepository = new UserRepositoryTests();
            userService = new UserService(mockUserRepository.MockUserRepository);
        }

        [TestMethod]
        public void Add_Any_AddToDB()
        {
            userService.Add(new User());
            Assert.AreEqual(userService.FindAll().Count, 4);
        }

        [TestMethod]
        public void FindAll_Returns3Users()
        {
            Assert.AreEqual(userService.FindAll().Count, 3);
        }

        [TestMethod]
        public void VerifyEmail_EmailInDB_ReturnsTrue()
        {
            Assert.IsTrue(userService.EmailExists("asd"));
        }

        [TestMethod]
        public void VerifyEmail_EmailNotInDB_ReturnsFalse()
        {
            Assert.IsFalse(userService.EmailExists("asdasdasdas"));
        }

        [TestMethod]
        public void VerifyAccount_VerifiedAccount_ReturnsFalse()
        {
            userService.Add(new User {IsEmailVerified = true, ActivationCode = new Guid("b5027fcc-da70-46f5-abf3-d1ae4c835df4") });
            Assert.IsFalse(userService.VerifyAccount("b5027fcc-da70-46f5-abf3-d1ae4c835df4"));
        }

        [TestMethod]
        public void VerifyAccount_NotVerifiedAccount_ReturnsTrue()
        {
            userService.Add(new User {Id=77, IsEmailVerified = false, ActivationCode = new Guid("b5027fcc-da70-46f5-abf3-d1ae4c835df4") });
            Assert.IsTrue(userService.VerifyAccount("b5027fcc-da70-46f5-abf3-d1ae4c835df4"));
        }

        [TestMethod]
        public void VerifyAccount_BadActivationCode_ReturnsFalse()
        {
            userService.Add(new User { Id=77, IsEmailVerified = false, ActivationCode = new Guid("b5027fcc-da70-46f5-abf3-d1ae4c835df5") });
            Assert.IsFalse(userService.VerifyAccount("b5027fcc-da70-46f5-abf3-d1ae4c835df4"));
        }

    }
}
