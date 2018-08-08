using BusinessTripApplication.Models;
using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Repository
{
    public interface IAreaRepository
    {
        IList<Area> FindAll();
    }
}