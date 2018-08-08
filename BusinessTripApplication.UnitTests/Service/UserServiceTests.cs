using BusinessTripModels.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.UnitTests.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessTripApplication.UnitTests.Service
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void Add_Any_AddToDB()
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
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            userService.Add(new User());
            IList<User> findedUsers = userService.FindAll();

            //Assert
            Assert.AreEqual(findedUsers.Count, users.Count);
            Assert.AreEqual(findedUsers.Count, 4);
        }

        [TestMethod]
        public void FindAll_Returns3Users()
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
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            IList<User> findedUsers = userService.FindAll();

            //Assert
            Assert.AreEqual(findedUsers.Count, 3);
            Assert.AreEqual(findedUsers.Count, users.Count);
        }

        [TestMethod]
        public void VerifyEmail_EmailInDB_ReturnsTrue()
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
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            bool isFinded = userService.EmailExists("asd");

            //Assert
            Assert.IsTrue(isFinded);
        }

        [TestMethod]
        public void VerifyEmail_EmailNotInDB_ReturnsFalse()
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
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            bool isFinded = userService.EmailExists("asdasdasdas");

            //Assert
            Assert.IsFalse(isFinded);
        }

        [TestMethod]
        public void VerifyAccount_VerifiedAccount_ReturnsFalse()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false},
                new User { IsEmailVerified = true, ActivationCode = new Guid("b5027fcc-da70-46f5-abf3-d1ae4c835df4") }
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByActivationCode(MockUserRepository, users);
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            bool isVerified = userService.VerifyAccount("b5027fcc-da70-46f5-abf3-d1ae4c835df4");

            //Assert
            Assert.IsFalse(isVerified);
        }

        [TestMethod]
        public void VerifyAccount_NotVerifiedAccount_ReturnsTrue()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false},
                new User { IsEmailVerified = true, ActivationCode = new Guid("b5027fcc-da70-46f5-abf3-d1ae4c835df4") }
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByActivationCode(MockUserRepository, users);
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            bool isVerified = userService.VerifyAccount("229c7b1b-309e-4d83-95b7-2f3e800403da");

            //Assert
            Assert.IsTrue(isVerified);
        }

        [TestMethod]
        public void VerifyAccount_BadActivationCode_ReturnsFalse()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=false, Name=""},
                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false},
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindByActivationCode(MockUserRepository, users);
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            bool isVerified = userService.VerifyAccount("b5027fcc-da70-46f5-abf3-d1ae4c835df4");

            //Assert
            Assert.IsFalse(isVerified);
        }

        [TestMethod]
        public void FindByEmail_BadEmail_ReturnsDefaultUser()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User{Id=1, Email="asd"}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindAll(MockUserRepository, users);
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            IList<User> findedUsers = userService.FindAll();
            User user = new User()
            {
                Email = "bad"
            };

            User findedUser = findedUsers.FirstOrDefault(u => u.Email == user.Email);

            //Assert
            Assert.AreNotEqual(findedUser, user);
            Assert.AreEqual(findedUser, default(User));
        }

        [TestMethod]
        public void FindByEmail_GoodEmail_ReturnsUser()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User{Id=1, Email="asd"}
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.FindAll(MockUserRepository, users);
            IUserService userService = new UserService(MockUserRepository.Object);

            //Act
            IList<User> findedUsers = userService.FindAll();
            User user = new User()
            {
                Email = "asd",
                Id = 2
            };

            User findedUser = findedUsers.FirstOrDefault(u => u.Email == user.Email);

            //Assert
            Assert.AreNotEqual(findedUser, user);
            Assert.AreEqual(findedUser, users[0]);
        }
    }
}
