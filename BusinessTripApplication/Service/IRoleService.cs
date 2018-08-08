using BusinessTripApplication.Models;
using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Service
{
    public interface IRoleService
    {
        Role FindByType(string type);
    }
}