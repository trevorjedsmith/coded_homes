using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodedHomes.Models;
using System.Data.Entity;
using CodedHomes.Data.Configuration;
using CodedHomes.DataContracts;

namespace CodedHomes.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Home> Homes { get; set; }

        public DataContext() : base("CodedHomes")
        { }

        public override int SaveChanges()
        {
            ApplyRules();
            return base.SaveChanges();
        }

        private void ApplyRules()
        {
            //this is good audit policy from Julie Lerman
            foreach (var entry in this.ChangeTracker.Entries().Where(
                e => e.Entity is IAuditInfo && (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                IAuditInfo homeEntity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    homeEntity.CreatedOn = DateTime.Now;
                }

                homeEntity.ModifiedOn = DateTime.Now;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HomeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            // Add ASP.NET WebPages SimpleSecurity tables
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new OAuthMembershipConfiguration());
            modelBuilder.Configurations.Add(new MembershipConfiguration());
            //base.OnModelCreating(modelBuilder);
        }
    }
}
