using BoilerplateData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoilerplateData.Context.Seed
{
    public static class UserSeed
    {
        public static List<User> Seed(BoilerplateContext context)
        {
            var seed = new List<User>();
            for (int i = 0; i < 20; i++)
            {
                seed.Add(new User() { Username = RandomString(6), EmailAddress = RandomString(10), PasswordHash = BCrypt.Net.BCrypt.HashPassword(RandomString(6)), Role = Role.GetUserRole(context) });
            }
            return seed;
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
