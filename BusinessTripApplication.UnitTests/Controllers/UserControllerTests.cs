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
        public readonly IUserService mockRepo;
        public UserControllerTests()
        {
            IList<User> users = new List<User>
            {
                new User(),
                new User{Id=1, Email=null, IsEmailVerified=true, Name=""},
                new User{Id=2, Email="test@test.com", IsEmailVerified=true, Name=""},

                new User{Email = "asd", ActivationCode= new Guid("229c7b1b-309e-4d83-95b7-2f3e800403da"), IsEmailVerified = false}
            };

            Mock<IUserService> mockRepo = new Mock<IUserService>();

            mockRepo.Setup(mr => mr.EmailExists(It.IsAny<string>())).Returns(
                (string email) => users.SingleOrDefault(x => x.Email == email) != null);

            mockRepo.Setup(mr => mr.VerifyAccount(It.IsAny<string>())).Returns(
                (string id) => users.SingleOrDefault(x => x.ActivationCode == new Guid(id)) != null);

            this.mockRepo = mockRepo.Object;
        }


        [TestMethod]
        public void Registration_RegisterUserWithAnEmailAlreadyUsed_StatusFalse()
        {
            //Arrange
            var dummyUser = new User("", "test@test.com", "");
            var controller = new UserController(mockRepo);

            var result = controller.Registration(dummyUser) as ViewResult;
            var model = result.Model as RegistrationViewModel;
            
            Assert.IsFalse(model.Status);

        }

        [TestMethod]
        public void Registration_RegisterUserWithValidData_StatusTrue()
        {
            var dummyUser = new User("Andrew", "cernovalex1@gmail.com", "");
            var controller = new UserController(mockRepo);

            var result = controller.Registration(dummyUser) as ViewResult;
            var model = result.Model as RegistrationViewModel;
            
            Assert.IsTrue(model.Status);
        }
    }
}