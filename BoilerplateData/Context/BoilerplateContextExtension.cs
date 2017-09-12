using BoilerplateData.Context.Seed;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoilerplateData.Context
{
    public static class BoilerplateContextExtension
    {
        public static bool AllMigrationsApplied(this BoilerplateContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this BoilerplateContext context)
        {

            if (!context.Roles.Any())
            {
                var roles = RolesSeed.Seed();
                context.AddRange(roles);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = UserSeed.Seed(context);
                context.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
