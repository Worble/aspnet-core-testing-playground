using BoilerplateData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerplateData.Context.Seed
{
    public static class RolesSeed
    {
        public static List<Role> Seed()
        {
            var seed = new List<Role>();
            seed.Add(new Role() { Name = "User" });
            seed.Add(new Role() { Name = "Admin" });
            return seed;
        }
    }
}
