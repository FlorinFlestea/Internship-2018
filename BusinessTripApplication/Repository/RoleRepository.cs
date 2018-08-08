using System.Collections.Generic;
using System.Linq;
using BusinessTripApplication.Models;
using BusinessTripModels.Models;
using BusinessTripModels.Exception;

namespace BusinessTripApplication.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public Role FindByType(string type)
        {
            Role role;
            using (DatabaseContext context = new DatabaseContext())
            {
               role = context.Roles.FirstOrDefault(x=>x.Type==type); ;
            }
            return role;
        }
    }
}