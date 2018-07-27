using BusinessTripApplication.Models;
using System.Collections.Generic;

namespace BusinessTripApplication.Repository
{
    public interface IAreaRepository
    {
        IList<Area> FindAll();
    }
}