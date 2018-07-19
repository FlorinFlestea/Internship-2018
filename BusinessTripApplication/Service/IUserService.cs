using BusinessTripApplication.Models;

namespace BusinessTripApplication.Repository
{
    public interface IUserService
    {
        User Add(User addedUser);
        bool EmailExists(string email);
        bool VerifyAccount(string id);
    }
}