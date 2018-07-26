using BusinessTripApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTripApplication.Service
{
    public interface IAreaService
    {
        IList<Area> FindAll();
    }
}