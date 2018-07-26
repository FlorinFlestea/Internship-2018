using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.ViewModels
{
    public interface IRegistrationViewModel
    {
        void SendVerificationLinkEmail(string emailId, string activationCode);
        bool CheckUser(IUserService userService, User user);
    }
}