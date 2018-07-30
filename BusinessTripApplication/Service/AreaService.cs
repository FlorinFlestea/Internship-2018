using System.Collections.Generic;
using System.Linq;
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

        public Area FindById(int Id)
        {
            IList<Area> areas = new List<Area>();
            try
            {
                areas = areaRepository.FindAll();
            }
            catch
            {
                throw;
            }

            Area returnArea = areas.Where(a => a.Id == Id);

            if (returnArea == default(Area))
                throw new DatabaseException("Area does't exists!\n");
            return returnArea;

        }
    }
}
