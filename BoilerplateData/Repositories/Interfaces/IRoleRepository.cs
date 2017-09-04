using BoilerplateData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerplateData.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Role GetUserRole();
    }
}
