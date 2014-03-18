using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Security;
using evoART.DAL;

namespace evoART.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("evoARTConnection")
        {
            Database.SetInitializer(new DatabaseContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            InitializeUserAccountsTable(modelBuilder);
            InitializeAccountValidationsTable(modelBuilder);
            InitializeRolesTable(modelBuilder);
        }

        protected virtual void InitializeUserAccountsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasKey(t => t.UserId)
                .Property(t => t.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasRequired(t => t.Role)
                .WithMany(t => t.UserAccounts)
                .Map(m => m.MapKey("RoleId"))
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasRequired(t => t.AccountValidation)
                .WithRequiredPrincipal(t => t.UserAccount)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AccountModels.UserAccount>()
                .Property(t => t.UserName).IsRequired();

            modelBuilder.Entity<AccountModels.UserAccount>()
                .Property(t => t.Email).IsRequired();

            modelBuilder.Entity<AccountModels.UserAccount>()
                .Property(t => t.Password).IsRequired();

            modelBuilder.Entity<AccountModels.UserAccount>()
                .Property(t => t.FistName).IsOptional();

            modelBuilder.Entity<AccountModels.UserAccount>()
                .Property(t => t.LastName).IsOptional();

            modelBuilder.Entity<AccountModels.UserAccount>()
                .Property(t => t.PhoneNumber).IsOptional();

            modelBuilder.Entity<AccountModels.UserAccount>()
                .Property(t => t.BirthDate).IsOptional();
        }

        private void InitializeAccountValidationsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModels.AccountValidation>()
                .HasKey(k => k.AccountValidationId);

            modelBuilder.Entity<AccountModels.AccountValidation>()
                .Property(p => p.IsVerified).IsRequired();

            modelBuilder.Entity<AccountModels.AccountValidation>()
                .Property(p => p.LoginFails).IsRequired();

            modelBuilder.Entity<AccountModels.AccountValidation>()
                .Property(p => p.ValidationToken).IsOptional();

            modelBuilder.Entity<AccountModels.AccountValidation>()
                .Property(p => p.ValidationTokenExpireDate).IsOptional();
        }

        private void InitializeRolesTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModels.Role>()
                .HasKey(t => t.RoleId)
                .Property(t => t.RoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AccountModels.Role>()
                .Property(p => p.RoleName).IsRequired();
        }

        public DbSet<AccountModels.UserAccount> UserAccounts { get; set; }

        public DbSet<AccountModels.AccountValidation> AccountValidations { get; set; }

        public DbSet<AccountModels.Role> Roles { get; set; }
    }
}