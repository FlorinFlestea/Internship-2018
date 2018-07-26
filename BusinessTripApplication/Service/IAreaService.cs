using BusinessTripApplication.Models;
using System.Collections.Generic;

namespace BusinessTripApplication.Service
{
    public interface IAreaService
    {
        IList<Area> FindAll();
    }
}