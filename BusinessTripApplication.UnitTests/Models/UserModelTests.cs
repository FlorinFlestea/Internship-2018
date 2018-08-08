using System;
using BusinessTripModels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTripApplication.UnitTests.Models
{
    [TestClass]
    public class UserModelTests
    {
        [TestMethod]
        public void User_CheckProperties_ReturnsTrue()
        {
            var user = new User("andrei", "andrei@yahoo.com", "pass");

            Assert.AreEqual(user.Name, "andrei");
            Assert.AreEqual(user.Email, "andrei@yahoo.com");
            Assert.AreEqual(user.Password, "pass");
            Assert.AreEqual(user.ActivationCode, Guid.Empty);
            Assert.AreEqual(user.IsEmailVerified, false);
        }
    }
}
