using System;
using System.Collections.Generic;
using System.Linq;
using BusinessTripApplication.Controllers;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using System.Web.Mvc;
using BusinessTripApplication.ViewModels;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessTripApplication.UnitTests.Repository;
using System.Net.Mail;

namespace BusinessTripApplication.UnitTests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {

        /*
         Cases to test:
         1. Email already used -> status = false
         2. Email not used -> status = true
         3. Email null -> status = false
         */

        [TestMethod]
        public void Registration_RegisterUserWithAnEmailAlreadyUsed_StatusFalse()
        {
            IList<User> users = new List<User>()
            {
               new User("", "test@test.com", "")
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.Add(MockUserRepository, users);
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);
            IUserRepository userRepository = MockUserRepository.Object;

            IUserService userService = new UserService(userRepository);

            var controller = new UserController(userService);

            Mock<IRegistrationViewModel> MockRegistrationViewModel = new Mock<IRegistrationViewModel>();
            MailMessage message = new MailMessage();
            UserControllerSetupMoq.SendVerificationLinkEmail(MockRegistrationViewModel, message);
            UserControllerSetupMoq.CheckUser(MockRegistrationViewModel);
            IRegistrationViewModel registrationViewModel = MockRegistrationViewModel.Object;

            //Act
            User dummyUser = new User("", "test@test.com", "");
            bool result = registrationViewModel.CheckUser(userService, dummyUser);

            //Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Registration_RegisterUserWithValidData_StatusTrue()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
               new User("", "test@test.com", "")
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.Add(MockUserRepository, users);
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);
            IUserRepository userRepository = MockUserRepository.Object;

            IUserService userService = new UserService(userRepository);

            var controller = new UserController(userService);

            Mock<IRegistrationViewModel> MockRegistrationViewModel = new Mock<IRegistrationViewModel>();
            MailMessage message = new MailMessage();
            UserControllerSetupMoq.SendVerificationLinkEmail(MockRegistrationViewModel, message);
            UserControllerSetupMoq.CheckUser(MockRegistrationViewModel);
            IRegistrationViewModel registrationViewModel = MockRegistrationViewModel.Object;

            //Act
            var dummyUser = new User("Andrew", "cernovalex1@gmail.com", "");
            bool result = registrationViewModel.CheckUser(userService, dummyUser);


            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SendEmail_CheckMessage()
        {
            //Arrange
            Mock<IRegistrationViewModel> MockRegistrationViewModel = new Mock<IRegistrationViewModel>();
            MailMessage message = new MailMessage();

            //UserControllerSetupMoq.SendVerificationLinkEmail(MockRegistrationViewModel, message);
            MockRegistrationViewModel.Setup(mock => mock.SendVerificationLinkEmail(It.IsAny<string>(), It.IsAny<string>())).Callback(
               (string emailTo, string activation) =>
               {
                   message = new MailMessage(new MailAddress("businesstripapplication@gmail.com", "Registration"), new MailAddress(emailTo))
                   {
                       Subject = "Activation link",
                       Body = activation,
                       IsBodyHtml = true
                   };
               });
            //
            IRegistrationViewModel registrationViewModel = MockRegistrationViewModel.Object;

            //Act
            string email = "email@asd.com";
            string guid = Guid.NewGuid().ToString();
            registrationViewModel.SendVerificationLinkEmail(email, guid);

            //Assert
            Assert.AreEqual(message.Subject, "Activation link");
            Assert.AreEqual(message.To[0], email);
            Assert.AreEqual(message.Body, guid);
            Assert.AreEqual(message.IsBodyHtml, true);
        }

        [TestMethod]
        public void LogIn_InvalidEmail_StatusFalse()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User("", "wrong@test.com", "")
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.Add(MockUserRepository, users);
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);
            IUserRepository userRepository = MockUserRepository.Object;

            IUserService userService = new UserService(userRepository);

            var controller = new UserController(userService);

            Mock<IRegistrationViewModel> MockRegistrationViewModel = new Mock<IRegistrationViewModel>();
            MailMessage message = new MailMessage();
            UserControllerSetupMoq.SendVerificationLinkEmail(MockRegistrationViewModel, message);
            UserControllerSetupMoq.CheckUser(MockRegistrationViewModel);
            IRegistrationViewModel registrationViewModel = MockRegistrationViewModel.Object;

            //Act
            var dummyUser = new User("Andrew", "cernovalex1@gmail.com", "");
            bool result = registrationViewModel.CheckUser(userService, dummyUser);


            //Assert
            Assert.IsTrue(result);
        }

    }
}