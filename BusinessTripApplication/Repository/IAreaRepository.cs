using BusinessTripApplication.Models;
using System.Collections.Generic;
using BusinessTripModels;

namespace BusinessTripApplication.Repository
{
    public interface IAreaRepository
    {
        IList<Area> FindAll();
    }
}