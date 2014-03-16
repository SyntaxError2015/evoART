using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace evoART.Models
{
    public class DatabaseWorkUnit : DbContext
    {
        public DatabaseWorkUnit() : base("evoARTConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseWorkUnit>());
        }

        public virtual void InitializeDatabaseTables()
        {
            UserAccounts.Create();
            AccountValidations.Create();
            Roles.Create();
        }

        public DbSet<AccountModels.UserAccount> UserAccounts { get; set; }

        public DbSet<AccountModels.AccountValidation> AccountValidations { get; set; }

        public DbSet<AccountModels.Role> Roles { get; set; }

        /*
        public class UserAccountsContext : DbContext
        {
            public UserAccountsContext()
                : base("evoARTConnection")
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<UserAccountsContext>());
            }

            /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }

            public DbSet<AccountModels.UserAccount> UserAccounts { get; set; }
        }

        public class AccountValidationContext : DbContext
        {
            public AccountValidationContext()
                : base("evoARTConnection")
            {

            }

            public DbSet<AccountModels.AccountValidation> AccountValidations { get; set; }
        }

        public class RolesContext : DbContext
        {
            public RolesContext()
                : base("evoARTConnection")
            {

            }

            public DbSet<AccountModels.Role> UserRoles { get; set; } 
        }*/
    }
}