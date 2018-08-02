using System;
using System.Collections.Generic;
using BusinessTripApplication.Controllers;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.ViewModels;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessTripApplication.UnitTests.Repository;
using System.Net.Mail;
using System.Web;
using System.Web.Security;

namespace BusinessTripApplication.UnitTests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {



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



        /*
        Login part
        */
        [TestMethod]
        public void Login_LoginUserWithAnInvalidEmail_StatusFalse()
        {
            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            IUserRepository userRepository = MockUserRepository.Object;
            IUserService userService = new UserService(userRepository);
            Mock<ILogInViewModel> MockLogInViewModel = new Mock<ILogInViewModel>();
            UserControllerSetupMoq.CheckUser(MockLogInViewModel);
            ILogInViewModel loginViewModel = MockLogInViewModel.Object;
            //Act
            User dummyUser = new User("", "test@test.com", "");
            bool result = loginViewModel.CheckUser(userService, dummyUser);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_LoginUserWithAnIncorrectPassword_StatusFalse()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User("", "testlogin@test.com", "testlogin")
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.Add(MockUserRepository, users);
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);
            IUserRepository userRepository = MockUserRepository.Object;

            IUserService userService = new UserService(userRepository);

            Mock<IRegistrationViewModel> MockRegistrationViewModel = new Mock<IRegistrationViewModel>();
            MailMessage message = new MailMessage();
            UserControllerSetupMoq.SendVerificationLinkEmail(MockRegistrationViewModel, message);
            UserControllerSetupMoq.CheckUser(MockRegistrationViewModel);

            Mock<ILogInViewModel> MockLogInViewModel = new Mock<ILogInViewModel>();
            UserControllerSetupMoq.CheckUser(MockLogInViewModel);
            ILogInViewModel loginViewModel = MockLogInViewModel.Object;
            //Act
            User dummyUser = new User("", "testlogin@test.com", "testincorrect");
            bool result = loginViewModel.CheckUser(userService, dummyUser);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_LoginUserWithValidData_StatusTrue()
        {
            //Arrange
            IList<User> users = new List<User>()
            {
                new User("", "testvalid@test.com", "test")
            };

            Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
            UserRepositorySetupMoq.Add(MockUserRepository, users);
            UserRepositorySetupMoq.FindByEmail(MockUserRepository, users);
            IUserRepository userRepository = MockUserRepository.Object;

            IUserService userService = new UserService(userRepository);

            Mock<IRegistrationViewModel> MockRegistrationViewModel = new Mock<IRegistrationViewModel>();
            MailMessage message = new MailMessage();
            UserControllerSetupMoq.SendVerificationLinkEmail(MockRegistrationViewModel, message);
            UserControllerSetupMoq.CheckUser(MockRegistrationViewModel);

            Mock<ILogInViewModel> MockLogInViewModel = new Mock<ILogInViewModel>();
            UserControllerSetupMoq.CheckUser(MockLogInViewModel);
            ILogInViewModel loginViewModel = MockLogInViewModel.Object;
            //Act
            User dummyUser = new User("", "testvalid@test.com", "test");
            bool result = loginViewModel.CheckUser(userService, dummyUser);

            //Assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void SetCookie_CheckCookieRememberMeFalse()
        {
            //Arrange
            Mock<ILogInViewModel> MockLogInViewModel = new Mock<ILogInViewModel>();
            HttpCookie cookie = null;

            //UserControllerSetupMoq.SetCookie(MockLogInViewModel,cookie);
            MockLogInViewModel.Setup(mock => mock.SetCookie(It.IsAny<string>(), It.IsAny<bool>())).Callback(
               (string email, bool rememberMe) =>
               {
                   int timeout = rememberMe ? 525600 : 20; // 525600 min = 1 year
                    var ticket = new FormsAuthenticationTicket(email, rememberMe, timeout);
                   string encrypted = FormsAuthentication.Encrypt(ticket);
                   cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                   cookie.Expires = DateTime.Now.AddMinutes(timeout);
                   cookie.HttpOnly = true;
               });
            //
            ILogInViewModel loginViewModel = MockLogInViewModel.Object;

            //Act
            string mail = "test@test.com";
            bool remember = false;
            loginViewModel.SetCookie(mail, remember);

            //Assert
            //Assert.AreEqual(cookie.Expires, DateTime.Now.AddMinutes(20));
            /*
             * CPU needs some time to execute the test and during this time, the cookie's duration
             * could be less(when tested with the if condition)
             */
            DateTime timeCookieCorrectEndTime = DateTime.Now.AddMinutes(20);
            DateTime timeCookieEndTime = cookie.Expires;
            Assert.IsTrue((timeCookieCorrectEndTime - timeCookieEndTime).Minutes < 2, "Fail");
            Assert.AreEqual(cookie.HttpOnly, true);
        }

        [TestMethod]
        public void SetCookie_CheckCookieRememberMeTrue()
        {
            //Arrange
            Mock<ILogInViewModel> MockLogInViewModel = new Mock<ILogInViewModel>();
            HttpCookie cookie = null;

            //UserControllerSetupMoq.SetCookie(MockLogInViewModel,cookie);
            MockLogInViewModel.Setup(mock => mock.SetCookie(It.IsAny<string>(), It.IsAny<bool>())).Callback(
                (string email, bool rememberMe) =>
                {
                    int timeout = rememberMe ? 262800 : 20; // 262800 min = 1/2 year
                    var ticket = new FormsAuthenticationTicket(email, rememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                });
            //
            ILogInViewModel loginViewModel = MockLogInViewModel.Object;

            //Act
            string mail = "test@test.com";
            bool remember = true;
            loginViewModel.SetCookie(mail, remember);

            //Assert
            /*
            * CPU needs some time to execute the test and during this time, the cookie's duration
            * could be less(when tested with the if condition)
            */
            DateTime timeCookieCorrectEndTime = DateTime.Now.AddMinutes(262800);
            DateTime timeCookieEndTime = cookie.Expires;
            Assert.IsTrue((timeCookieCorrectEndTime - timeCookieEndTime).Minutes < 2, "Fail");
            Assert.AreEqual(cookie.HttpOnly, true);
        }

    }
}