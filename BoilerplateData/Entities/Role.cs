using System;
using System.Collections.Generic;
using System.Text;
using BoilerplateData.Context;
using System.Linq;

namespace BoilerplateData.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        internal static Role GetUserRole(BoilerplateContext context)
        {
            return context.Roles.Where(r => r.Name == "User").FirstOrDefault(); 
        }
    }
}
