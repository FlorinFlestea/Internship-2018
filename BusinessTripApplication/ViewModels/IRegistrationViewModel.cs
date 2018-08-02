using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripModels;

namespace BusinessTripApplication.ViewModels
{
    public interface IRegistrationViewModel
    {
        bool CheckUser(IUserService userService, User user);
    }
}