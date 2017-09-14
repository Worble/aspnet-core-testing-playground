using BoilerplateData.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace BoilerplateData.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Register(string username, string emailAddress, string password);
        bool CheckUniqueUsername(string username);
        bool CheckUniqueEmail(string emailAddress);
        UserDTO Login(string emailAddressOrUsername, string password);
        void EditEmail(int id, string emailAddress);
        UserDTO Get(int id);
        bool ValidateLastChanged(int userID, string lastChanged);
        List<UserDTO> GetPage(int pageNumber, int resultAmount);
        int GetTotalPages(int resultAmount);
    }
}
