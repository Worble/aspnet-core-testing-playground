using BoilerplateData.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerplateData.Work.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }

        void Save();
    }
}
