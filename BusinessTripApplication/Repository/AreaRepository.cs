using System.Collections.Generic;
using System.Linq;
using BusinessTripApplication.Models;
using BusinessTripModels.Models;
using BusinessTripModels.Exception;

namespace BusinessTripApplication.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IList<Area> FindAll()
        {
            IList<Area> areas = new List<Area>();

            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    areas = context.Areas.ToList();
                }
            }
            catch (System.Exception e)
            {
                Logger.Info(e.Message);
                throw new DatabaseException("Cannot connect to database!\n");
            }

            return areas;
        }
    }
}