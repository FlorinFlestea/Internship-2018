using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.ViewModels
{
    public interface IRegistrationViewModel
    {
        bool CheckUser(IUserService userService, User user);
    }
}