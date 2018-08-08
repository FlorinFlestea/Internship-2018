using BusinessTripApplication.Models;
using System.Collections.Generic;
using BusinessTripModels.Models;

namespace BusinessTripApplication.Service
{
    public interface IAreaService
    {
        IList<Area> FindAll();
        Area FindById(int Id);
    }
}