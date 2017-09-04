using BoilerplateData.Context;
using BoilerplateData.Repositories;
using BoilerplateData.Repositories.Interfaces;
using BoilerplateData.Work.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerplateData.Work
{
    public class UnitOfWork : IUnitOfWork
    {
        private BoilerplateContext context;
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;

        private bool disposed = false;

        public UnitOfWork()
        {
            this.context = new BoilerplateContext();
        }

        public IUserRepository UserRepository { get
            {
                if(this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.context);
                }
                return this.userRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new RoleRepository(this.context);
                }
                return this.roleRepository;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
