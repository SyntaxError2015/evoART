using System.ComponentModel.DataAnnotations.Schema;
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
            InitializeUserAccountsTable(modelBuilder);
            InitializeAccountValidationsTable(modelBuilder);
            InitializeRolesTable(modelBuilder);
            InitializeSessionsTable(modelBuilder);
            InitializeOAuthLoginsTable(modelBuilder);
        }

        protected virtual void InitializeUserAccountsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasKey(t => t.UserId)
                .Property(t => t.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Map foreign key for the Roles table
            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasRequired(t => t.Role)
                .WithMany(t => t.UserAccounts)
                .Map(m => m.MapKey("RoleId"))
                .WillCascadeOnDelete(false);

            // Map foreign key for the AccountValidations table
            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasOptional(t => t.AccountValidation)
                .WithOptionalPrincipal(p => p.UserAccount)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            // Map foreign key for the Sessions table
            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasOptional(t => t.Session)
                .WithOptionalPrincipal(p => p.UserAccount)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            // Map foreign ket for the OAuthLogin table
            modelBuilder.Entity<AccountModels.UserAccount>()
                .HasOptional(t => t.OAuthLogins)
                .WithOptionalPrincipal(t => t.UserAccounts)
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

        private static void InitializeAccountValidationsTable(DbModelBuilder modelBuilder)
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

        private static void InitializeRolesTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModels.Role>()
                .HasKey(t => t.RoleId)
                .Property(t => t.RoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AccountModels.Role>()
                .Property(p => p.RoleName).IsRequired();
        }

        private static void InitializeSessionsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModels.Session>()
                .HasKey(k => k.SessionId);

            modelBuilder.Entity<AccountModels.Session>()
                .Property(p => p.SessionKey).IsRequired();
        }

        private static void InitializeOAuthLoginsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModels.OAuthLogin>()
                .HasKey(k => k.OAuthLoginId)
                .Property(p => p.OAuthLoginId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<AccountModels.OAuthLogin>()
                .Property(p => p.Provider).IsRequired();

            modelBuilder.Entity<AccountModels.OAuthLogin>()
                .Property(p => p.Key).IsRequired();
        }

        public DbSet<AccountModels.UserAccount> UserAccounts { get; set; }

        public DbSet<AccountModels.AccountValidation> AccountValidations { get; set; }

        public DbSet<AccountModels.Role> Roles { get; set; }

        public DbSet<AccountModels.Session> Sessions { get; set; }

        public DbSet<AccountModels.OAuthLogin> OAuthLogins { get; set; }
    }
}