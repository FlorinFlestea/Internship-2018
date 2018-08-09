
using System;
using System.Net.Mail;
using System.Reflection;
using System.Security.Policy;
using BusinessTripApplication.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessTripApplication.UnitTests.Server
{
    [TestClass]  
    public class FacebookLoginerTests
    {
       
        [TestMethod]
        public void Response_CheckMessage()
        {
            //Arrange
            Mock<IFacebookLoginer> MockFacebookLoginer = new Mock<IFacebookLoginer>();
            String appId = "1937273663237215";
            String appSecret = "66e4c89da64be8e1d96e29bfec738324";

                MockFacebookLoginer.Setup(mock => mock.SetLoginUrl(It.IsAny<Uri>())).Returns(
            (Uri redirectUri) =>
            {
                var loginUrl = new
                {
                    client_id = appId,
                    client_secret = appSecret,
                    redirect_uri = redirectUri.AbsoluteUri,
                    response_type = "code",
                    scope = "email" // Add other permissions as needed
                };
                return loginUrl;
            });
            

            IFacebookLoginer facebookLoginer = MockFacebookLoginer.Object;

            //Act
            Uri redirecUri = new Uri("https://localhost");
            var logUrl = facebookLoginer.SetLoginUrl(redirecUri);

            Type t = logUrl.GetType();
            PropertyInfo clientIdProperty = t.GetProperty("client_id");
            PropertyInfo clientSecretProperty = t.GetProperty("client_secret");
            PropertyInfo responseTypeProperty = t.GetProperty("response_type");
            PropertyInfo scopeProperty = t.GetProperty("scope");
            string clientId = (string) clientIdProperty.GetValue(logUrl, null);
            string clientSecret = (string)clientSecretProperty.GetValue(logUrl, null);
            string responseType = (string)responseTypeProperty.GetValue(logUrl, null);
            string scope = (string) scopeProperty.GetValue(logUrl, null);
            //Assert
            Assert.AreEqual(clientId, appId);
            Assert.AreEqual(clientSecret, appSecret);
            Assert.AreEqual(responseType, "code");
            Assert.AreEqual(scope, "email");
        }
    }
}
