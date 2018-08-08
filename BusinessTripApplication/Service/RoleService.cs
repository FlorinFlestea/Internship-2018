using System.Collections.Generic;
using System.Linq;
using BusinessTripApplication.Repository;
using BusinessTripModels.Models;
using BusinessTripModels.Exception;

namespace BusinessTripApplication.Service
{
    public class RoleService : IRoleService
    {
        readonly IRoleRepository roleRepository;

        public RoleService()
        {
            this.roleRepository = new RoleRepository();
        }

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public Role FindByType(string type)
        {
            Role returnRole = roleRepository.FindByType(type);
            return returnRole;
        }
    }
}
