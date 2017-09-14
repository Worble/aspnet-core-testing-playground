using System;
using System.Collections.Generic;
using System.Text;
using BoilerplateData.Context;
using System.Linq;
using BoilerplateData.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BoilerplateData.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string PasswordHash { get; set; }

        public virtual Role Role { get; set; }

        internal static void Register(BoilerplateContext context, string username, string emailAddress, string password)
        {
            context.Users.Add(new User() { Username = username, EmailAddress = emailAddress, PasswordHash = BCrypt.Net.BCrypt.HashPassword(password), Role = Role.GetUserRole(context) });
        }

        internal static bool CheckUniqueUsername(BoilerplateContext context, string username)
        {
            if (context.Users.Where(u => u.Username == username).Count() > 0)
            {
                return false;
            }
            return true;
        }

        internal static void EditEmail(BoilerplateContext context, int id, string emailAddress)
        {
            var userToEdit = context.Users.Find(id);
            userToEdit.EmailAddress = emailAddress;
            context.Users.Attach(userToEdit);
            context.Entry(userToEdit).State = EntityState.Modified;
        }

        internal static UserDTO Get(BoilerplateContext context, int id)
        {
            var user = context.Users.Find(id);
            context.Entry(user).Reference(e => e.Role).Load();
            return new UserDTO() { ID = user.ID, EmailAddress = user.EmailAddress, Role = user.Role.Name, Username = user.Username, LastUpdated = user.EditedDate ?? user.CreatedDate };
        }

        internal static List<UserDTO> GetPage(BoilerplateContext context, int pageNumber, int resultAmount)
        {
            int resultsToSkip = pageNumber * resultAmount;
            return context.Users
                .OrderBy(e => e.CreatedDate)
                .Skip(resultsToSkip)
                .Take(resultAmount)
                .Select(e => new UserDTO()
                    {
                        EmailAddress = e.EmailAddress,
                        ID = e.ID,
                        LastUpdated = e.EditedDate ?? e.CreatedDate,
                        Role = e.Role.Name,
                        Username = e.Username
                    })
                .ToList();
        }

        internal static int GetTotalPages(BoilerplateContext context, int resultAmount)
        {
            return context.Users.Count() / resultAmount;
        }

        internal static bool ValidateLastChanged(BoilerplateContext context, int userID, string lastChanged)
        {
            var user = context.Users.Find(userID);
            string changedTime = user.EditedDate.ToString() ?? user.CreatedDate.ToString();
            return changedTime == lastChanged;
        }

        internal static bool CheckUniqueEmailAddress(BoilerplateContext context, string emailAddress)
        {
            if (context.Users.Where(u => u.EmailAddress == emailAddress).Count() > 0)
            {
                return false;
            }
            return true;
        }

        internal static UserDTO Login(BoilerplateContext context, string usernameOrEmailAddress, string password)
        {
            var user = context.Users
                .Include(u => u.Role)
                .Where(u => u.Username == usernameOrEmailAddress || u.EmailAddress == usernameOrEmailAddress)                
                .FirstOrDefault();
            if(user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new UserDTO() { EmailAddress = user.EmailAddress, ID = user.ID, Username = user.Username, Role = user.Role.Name };
            }
            return null;
        }
    }
}
