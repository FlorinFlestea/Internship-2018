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
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(mr => mr.FindByEmail(It.IsAny<string>())).Returns(
                (string email) => users.Where(x => x.Email == email).SingleOrDefault());

            mockUserRepository.Setup(mr => mr.FindByActivationCode(It.IsAny<Guid>())).Returns(
                (Guid id) => users.Where(x => x.ActivationCode == id).SingleOrDefault());

            mockUserRepository.Setup(mr => mr.FindAll()).Returns(
                users);

            mockUserRepository.Setup(mr => mr.UpdateIsEmailVerified(It.IsAny<User>())).Returns(
                (User update) =>
                {
                    User user = users.SingleOrDefault(x => x.Id == update.Id);

                    if (user != null)
                    {
                        user.IsEmailVerified = update.IsEmailVerified;
                    }
                    return user;
                });

            mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Returns(
                (User addedUser) =>
                {
                    users.Add(addedUser);
                    return addedUser;
                });

            this.MockUserRepository = mockUserRepository.Object;
        }

        [TestMethod]
        public void FindByEmail_Good_ReturnUser()
        {
            string email = "asd";
            User result = MockUserRepository.FindByEmail(email);

            Assert.AreEqual(result.Email, email);
        }

        [TestMethod]
        public void FindByEmail_Bad_ReturnsNull()
        {
            string email = "badEmail";
            User result = MockUserRepository.FindByEmail(email);

            Assert.AreEqual(result, null);
        }

        public void FindByActivationCode_Good_ReturnsUser()
        {
            Guid code = new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da");
            User result = MockUserRepository.FindByActivationCode(code);

            Assert.AreEqual(result.Email, code);
        }

        [TestMethod]
        public void FindByActivationCode_Bad_ReturnsNull()
        {
            Guid code = new Guid("229c7b1b-309e-4d83-95b7-2f3e800403bb");
            User result = MockUserRepository.FindByActivationCode(code);

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void FindAll_ReturnsAllUsers()
        {
            IList<User> users = MockUserRepository.FindAll();

            Assert.AreEqual(users.Count, 3);
        }

        [TestMethod]
        public void Update_BadUser_Nothing()
        {
            User user = new User()
            {
                Id = 999,
                Name = "asd",
                ActivationCode = Guid.NewGuid(),
                Email = "email",
                IsEmailVerified = false,
                Password = "pass"
            };

            User result = MockUserRepository.UpdateIsEmailVerified(user);

            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void Update_GodUser_ModifyDataBase()
        {
            User user = new User()
            {
                Id = 1,
                Name = "asd",
                ActivationCode = Guid.NewGuid(),
                Email = "email",
                IsEmailVerified = true,
                Password = "pass"
            };

            User result = MockUserRepository.UpdateIsEmailVerified(user);

            Assert.AreEqual(result.IsEmailVerified, user.IsEmailVerified);

            User resultFromDB = MockUserRepository.FindAll().Where(x => x.Id == user.Id).FirstOrDefault();
            Assert.AreEqual(resultFromDB.IsEmailVerified, true);
        }

        [TestMethod]
        public void Add_Any_Modify()
        {
            User user = new User()
            {
                Id = 999,
                Name = "asd",
                ActivationCode = Guid.NewGuid(),
                Email = "email",
                IsEmailVerified = false,
                Password = "pass"
            };
            User result = MockUserRepository.Add(user);
            Assert.AreEqual(result, user);
            Assert.AreEqual(MockUserRepository.FindAll().Count, 4);

            user = null;
            result = MockUserRepository.Add(user);
            Assert.AreEqual(result, null);
            Assert.AreEqual(MockUserRepository.FindAll().Count, 5);
        }


    }
}
