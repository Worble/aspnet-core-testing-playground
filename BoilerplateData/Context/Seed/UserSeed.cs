using BoilerplateData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoilerplateData.Context.Seed
{
    public static class UserSeed
    {
        public static void Seed(BoilerplateContext context)
        {
            int users = context.Users.Count();
            for (int i = users; i < 200; i++)
            {
                context.Users.Add(new User() { Username = RandomString(6), EmailAddress = RandomString(6) + "@" + RandomString(4) + ".com", PasswordHash = BCrypt.Net.BCrypt.HashPassword(RandomString(6)), Role = Role.GetUserRole(context) });
            }
            return;
        }

        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
