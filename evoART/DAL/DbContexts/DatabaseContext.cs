﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public class DatabaseContext : DbContext
    {
        protected internal DatabaseContext()
            : base("evoARTConnection")
        {
            Database.SetInitializer(new DatabaseContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            UserAccountsInitializer.InitializeUserAccountsModels(modelBuilder);
        }

        public DbSet<AccountModels.UserAccount> UserAccounts { get; set; }

        public DbSet<AccountModels.AccountValidation> AccountValidations { get; set; }

        public DbSet<AccountModels.Role> Roles { get; set; }

        public DbSet<AccountModels.Session> Sessions { get; set; }

        public DbSet<AccountModels.OAuthLogin> OAuthLogins { get; set; }
    }
}