using BusinessTripApplication.Models;
using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Repository
{
    public interface IRoleRepository
    {
        Role FindByType(string type);
    }
}