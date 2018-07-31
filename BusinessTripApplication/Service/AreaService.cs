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

            return areaRepository.FindAll(); 
        }

        public Area FindById(int Id)
        {
            IList<Area> areas = areaRepository.FindAll();

            Area returnArea = areas.Where(a => a.Id == Id).FirstOrDefault();

            if (returnArea == default(Area))
                throw new DatabaseException("Area does't exists!\n");
            return returnArea;

        }
    }
}
