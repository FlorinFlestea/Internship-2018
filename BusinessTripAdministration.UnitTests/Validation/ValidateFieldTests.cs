using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessTripAdministration.Validation;

namespace BusinessTripAdministration.UnitTests.Validation
{
    [TestClass]
    public class ValidateFieldTests
    {
        
        [TestMethod]
        public void Email_GoodFormat_ReturnTrue()
        {
            string email = "sabau@yahoo.com";
            string message = "";
            bool result = ValidateField.Email(email, ref message);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Email_WrongFormat_ReturnFalse()
        {
            string email = "sabau####";
            string message = "";
            bool result = ValidateField.Email(email, ref message);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Email_WrongFormat_ReturnFalse1()
        {
            string email = "@yahoo.com";
            string message = "";
            bool result = ValidateField.Email(email, ref message);
            Assert.IsFalse(result);
        }


    }
}
