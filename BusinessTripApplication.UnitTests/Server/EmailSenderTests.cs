using System;
using System.Net.Mail;
using BusinessTripApplication.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessTripApplication.Server;
using Moq;

namespace BusinessTripApplication.UnitTests.Server
{
    [TestClass]
    public class EmailSenderTests
    {
        [TestMethod]
        public void SendEmail_CheckMessage()
        {
            
            //Arrange
            Mock<IEmailSender> MockEmailSender = new Mock<IEmailSender>();
            MailMessage message = new MailMessage();

            MockEmailSender.Setup(mock => mock.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(
                (string emailTo, string subject, string body) =>
                {
                    message = new MailMessage(new MailAddress("businesstripapplication@gmail.com"), new MailAddress(emailTo))
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                });
            //
            
            IEmailSender emailSender = MockEmailSender.Object;

            //Act
            string email = "andrei@yahoo.com";
            string sub = "Activation";
            string msg = "ttt";
            emailSender.SendEmail(email, sub, msg);

            //Assert
            Assert.AreEqual(message.Subject, sub);
            Assert.AreEqual(message.To[0], email);
            Assert.AreEqual(message.Body, msg);
            Assert.AreEqual(message.IsBodyHtml, true);
        }
    }
}
