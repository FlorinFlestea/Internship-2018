using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripModels;

namespace BusinessTripApplication.ViewModels
{
    public interface IRegistrationViewModel
    {
        void SendVerificationLinkEmail(string emailId, string activationCode);
        bool CheckUser(IUserService userService, User user);
    }
}