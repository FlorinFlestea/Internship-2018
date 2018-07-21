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
        public readonly IUserService service;
        public UserControllerTests()
        {        
            this.service = new UserService(new UserRepositoryTests().MockUserRepository);
        }


        [TestMethod]
        public void Registration_RegisterUserWithAnEmailAlreadyUsed_StatusFalse()
        {
            //Arrange
            var dummyUser = new User("", "test@test.com", "");
            service.Add(dummyUser);
            var controller = new UserController(service);

            var result = controller.Registration(dummyUser) as ViewResult;
            var model = result.Model as RegistrationViewModel;
            
            Assert.IsFalse(model.Status);

        }

        [TestMethod]
        public void Registration_RegisterUserWithValidData_StatusTrue()
        {
            var dummyUser = new User("Andrew", "cernovalex1@gmail.com", "");
            var controller = new UserController(service);

            var result = controller.Registration(dummyUser) as ViewResult;
            var model = result.Model as RegistrationViewModel;
            
            Assert.IsTrue(model.Status);
        }
    }
}