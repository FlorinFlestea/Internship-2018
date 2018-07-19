using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessTripApplication.UnitTests.Repository
{
    [TestClass]
    public class UserRepositoryTests
    {
        public readonly IUserRepository MockUserRepository;
        public UserRepositoryTests()
        {
            IList<User> users = new List<User>
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=true, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(mr => mr.FindByEmail(It.IsAny<string>())).Returns(
                (string email) => users.Where(x => x.Email == email).SingleOrDefault());

            mockUserRepository.Setup(mr => mr.FindByActivationCode(It.IsAny<Guid>())).Returns(
                (Guid id) => users.Where(x => x.ActivationCode == id).SingleOrDefault());

            this.MockUserRepository = mockUserRepository.Object;
        }

        [TestMethod]
        public void UserRepository_GoodEmail_ReturnUser()
        {
            string email = "asd";
            User result = MockUserRepository.FindByEmail(email);

            Assert.AreEqual(result.Email, email);
        }

        [TestMethod]
        public void UserRepository_BadEmail_ReturnNull()
        {
            string email = "badEmail";
            User result = MockUserRepository.FindByEmail(email);

            Assert.AreEqual(result, null);
        }

        public void UserRepository_GoodActivationCode_ReturnUser()
        {
            Guid code = new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da");
            User result = MockUserRepository.FindByActivationCode(code);

            Assert.AreEqual(result.Email, code);
        }

        [TestMethod]
        public void UserRepository_BadActivationCode_ReturnNull()
        {
            Guid code = new Guid("229c7b1b-309e-4d83-95b7-2f3e800403bb");
            User result = MockUserRepository.FindByActivationCode(code);

            Assert.AreEqual(result, null);
        }
    }
}
