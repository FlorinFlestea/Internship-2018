using System.Collections.Generic;
using BusinessTripApplication.Models;
using BusinessTripApplication.Repository;

namespace BusinessTripApplication.Service
{
    public class AreaService : IAreaService
    {
        readonly IAreaRepository areaRepository;

        public AreaService()
        {
            this.areaRepository = new AreaRepository();
        }

        public AreaService(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public IList<Area> FindAll()
        {
            try
            {
                return areaRepository.FindAll(); 
            }
            catch
            {
                throw;
            }
        }
    }
}