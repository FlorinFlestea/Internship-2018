using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.ViewModels;
using Moq;
using System.Net.Mail;
using System.Web.Helpers;

namespace BusinessTripApplication.UnitTests.Controllers
{
    public static class UserControllerSetupMoq
    {
        public static void SendVerificationLinkEmail(Mock<IRegistrationViewModel> MockRegistrationViewModel, MailMessage message)
        {
            MockRegistrationViewModel.Setup(mock => mock.SendVerificationLinkEmail(It.IsAny<string>(), It.IsAny<string>())).Callback(
               (string email, string activation) =>
               {
                   message = new MailMessage(new MailAddress("businesstripapplication@gmail.com", "Registration"), new MailAddress(email))
                   {
                       Subject = "Activation link",
                       Body = activation,
                       IsBodyHtml = true
                   };
               });
        }

        public static void CheckUser(Mock<IRegistrationViewModel> MockRegistrationViewModel)
        {
            MockRegistrationViewModel.Setup(mock => mock.CheckUser(It.IsAny<IUserService>(), It.IsAny<User>())).Returns(
                (IUserService userService, User user) =>
                {
                    bool emailExists = userService.EmailExists(user.Email);

                    if (emailExists)
                        return false;

                    User addedUser = userService.Add(user);
                    addedUser.Password = "";

                    MockRegistrationViewModel.Object.SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());

                    return true;
                });
        }

        public static void CheckUser(Mock<ILogInViewModel> MockLoginViewModel)
        {
                MockLoginViewModel.Setup(mock => mock.CheckUser(It.IsAny<IUserService>(), It.IsAny<User>())).Returns(
                (IUserService userService, User user) =>
                {
                    bool emailExists = userService.EmailExists(user.Email);

                    if (emailExists==false)
                        return false;

                    User addedUser = userService.GetUserByEmail(user.Email);
                    if ((addedUser.Password == Crypto.Hash(user.Password))==false)
                        return false;
                    bool rememberMe = false;

                    MockLoginViewModel.Object.SetCookie(user.Email, rememberMe);

                    return true;
                });
        }

    }
}
