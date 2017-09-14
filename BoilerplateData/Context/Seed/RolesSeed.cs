using BoilerplateData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoilerplateData.Context.Seed
{
    public static class RolesSeed
    {
        public static void Seed(BoilerplateContext context)
        {
            if(context.Roles.Where(e => e.Name == "User").FirstOrDefault() == null)
            {
                context.Roles.Add(new Role() { Name = "User" });
            }
            if (context.Roles.Where(e => e.Name == "Admin").FirstOrDefault() == null)
            {
                context.Roles.Add(new Role() { Name = "Admin" });
            }
        }
    }
}
