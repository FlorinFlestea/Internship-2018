using BusinessTripApplication.Models;

namespace BusinessTripApplication.Repository
{
    public interface IUserRepository
    {
        User Add(User addedUser);
        bool EmailExists(string email);
    }
}