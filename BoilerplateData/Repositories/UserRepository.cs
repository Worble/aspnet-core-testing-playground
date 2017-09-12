using BoilerplateData.Context;
using BoilerplateData.Entities;
using BoilerplateData.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BoilerplateData.DTOs;
using System.Security.Claims;

namespace BoilerplateData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BoilerplateContext context;

        public UserRepository(BoilerplateContext context)
        {
            this.context = context;
        }

        public bool CheckUniqueEmail(string emailAddress)
        {
            return User.CheckUniqueEmailAddress(context, emailAddress);
        }

        public bool CheckUniqueUsername(string username)
        {
            return User.CheckUniqueUsername(context, username);
        }

        public void EditEmail(int id, string emailAddress)
        {
            User.EditEmail(context, id, emailAddress);
        }

        public UserDTO Login(string emailAddressOrUsername, string password)
        {
            return User.Login(context, emailAddressOrUsername, password);
        }

        public void Register(string username, string emailAddress, string password)
        {
            User.Register(context, username, emailAddress, password);
        }

        public UserDTO Get(int id)
        {
            return User.Get(context, id);
        }

        public bool ValidateLastChanged(int userID, string lastChanged)
        {
            return User.ValidateLastChanged(context, userID, lastChanged);
        }

        public List<UserDTO> GetAllPaged(int pageNumber, int resultAmount)
        {
            return User.GetAllPaged(context, pageNumber, resultAmount);
        }
    }
}
