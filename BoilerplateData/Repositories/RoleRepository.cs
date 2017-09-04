using BoilerplateData.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BoilerplateData.Entities;
using BoilerplateData.Context;

namespace BoilerplateData.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private BoilerplateContext context;

        public RoleRepository(BoilerplateContext context)
        {
            this.context = context;
        }

        public Role GetUserRole()
        {
            return Role.GetUserRole(context);
        }
    }
}
