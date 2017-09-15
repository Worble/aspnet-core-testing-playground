using BoilerplateData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoilerplateData.Context
{
    public class BoilerplateContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //remove this in production, use global variable instead
            optionsBuilder.UseNpgsql(@"User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=AspNetBoilerplate;Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Role>().HasIndex(e => e.Name).IsUnique();

            modelBuilder.Entity<User>().Property(e => e.Username).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.EmailAddress).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.PasswordHash).IsRequired();

            modelBuilder.Entity<User>().HasOne(e => e.Role).WithMany().IsRequired();

            modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.EmailAddress).IsUnique();
        }

        public override int SaveChanges()
        {
            var changes = from e in this.ChangeTracker.Entries<BaseEntity>()
                          where e.State != EntityState.Unchanged
                          select e;
            foreach (var change in changes)
            {
                if (change.State == EntityState.Added)
                {
                    change.Entity.CreatedDate = DateTime.UtcNow;
                }
                else if (change.State == EntityState.Modified)
                {
                    change.Entity.EditedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }
    }
}
