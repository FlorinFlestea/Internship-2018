using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessTripApplication.UnitTests.Repository
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void FindByEmail_Good_ReturnUser()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);

            //Act
            string email = "asd";
            User result = MockUserRepository.Object.FindByEmail(email);

            //Assert
            Assert.AreEqual(result.Email, email);
        }

        [TestMethod]
        public void FindByEmail_Bad_ReturnsNull()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);

            //Act
            string email = "badEmail";
            User result = MockUserRepository.Object.FindByEmail(email);

            //Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void FindByActivationCode_Good_ReturnsUser()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByActivationCode(MockUserRepository, users);

            //Act
            Guid code = new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da");
            User result = MockUserRepository.Object.FindByActivationCode(code);

            //Assert
            Assert.AreEqual(result.ActivationCode, code);
        }

        [TestMethod]
        public void FindByActivationCode_Bad_ReturnsNull()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByActivationCode(MockUserRepository, users);

            //Act
            Guid code = new Guid("229c7b1b-309e-4d83-95b7-2f3e800403bb");
            User result = MockUserRepository.Object.FindByActivationCode(code);

            //Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void FindAll_ReturnsAllUsers()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindAll(MockUserRepository, users);

            //Act
            IList<User> findedUsers = MockUserRepository.Object.FindAll();

            Assert.AreEqual(users.Count, findedUsers.Count);
        }

        [TestMethod]
        public void Update_BadUser_Nothing()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.UpdateIsEmailVerified(MockUserRepository, users);

            //Act
            User user = new User()
            {
                Id = 999,
                Name = "asd",
                ActivationCode = Guid.NewGuid(),
                Email = "email",
                IsEmailVerified = false,
                Password = "pass"
            };
            User result = MockUserRepository.Object.UpdateIsEmailVerified(user);

            //Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void Update_GodUser_ModifyDataBase()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.UpdateIsEmailVerified(MockUserRepository, users);
            UserRepositorySetupMoq.FindAll(MockUserRepository, users);

            //Act
            User user = new User()
            {
                Id = 1,
                Name = "asd",
                ActivationCode = Guid.NewGuid(),
                Email = "email",
                IsEmailVerified = true,
                Password = "pass"
            };

            User result = MockUserRepository.Object.UpdateIsEmailVerified(user);
            User resultFromDB = MockUserRepository.Object.FindAll().Where(x => x.Id == user.Id).FirstOrDefault();

            //Assert
            Assert.AreEqual(result.IsEmailVerified, user.IsEmailVerified);
            Assert.AreEqual(resultFromDB.IsEmailVerified, true);
        }

        [TestMethod]
        public void Add_Any_Modify()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.Add(MockUserRepository, users);
            UserRepositorySetupMoq.FindAll(MockUserRepository, users);

            //Act
            User user = new User()
            {
                Id = 999,
                Name = "asd",
                ActivationCode = Guid.NewGuid(),
                Email = "email",
                IsEmailVerified = false,
                Password = "pass"
            };
            User result = MockUserRepository.Object.Add(user);
            IList<User> findedUsers = MockUserRepository.Object.FindAll();

            //Assert
            Assert.AreEqual(result, user);
            Assert.AreEqual(findedUsers.Count, users.Count);
        }


    }
}
