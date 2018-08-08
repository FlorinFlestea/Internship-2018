using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;
using BusinessTripApplication.Service;
using BusinessTripModels.Models;

namespace BusinessTripApplication.ViewModels
{
    public interface IRegistrationViewModel
    {
        bool CheckUser(IUserService userService, IRoleService roleService, User user);
    }
}