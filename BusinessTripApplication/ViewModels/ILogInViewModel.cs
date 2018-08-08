using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripModels.Models;

namespace BusinessTripApplication.ViewModels
{
    public interface ILogInViewModel
    {
        bool CheckUser(IUserService userService, User user);
        void SetCookie(string email, bool rememberMe);
    }
}