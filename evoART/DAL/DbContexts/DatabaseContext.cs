﻿using System.Data.Entity;
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

            UserAccountsContextInitializer.InitializeUserAccountsModels(modelBuilder);
            PhotosContextInitializer.InitializePhotosModels(modelBuilder);
            SocialContextInitializer.InitializeSocialModels(modelBuilder);
        }

        #region User accounts
        
        public DbSet<AccountModels.UserAccount> UserAccounts { get; set; }

        public DbSet<AccountModels.AccountValidation> AccountValidations { get; set; }

        public DbSet<AccountModels.Role> Roles { get; set; }

        public DbSet<AccountModels.Session> Sessions { get; set; }

        public DbSet<AccountModels.OAuthLogin> OAuthLogins { get; set; }

        #endregion

        #region Photos

        public DbSet<PhotoModels.Photo> Photos { get; set; }

        public DbSet<PhotoModels.Album> Albums { get; set; }

        public DbSet<PhotoModels.HashTag> HashTags { get; set; }

        public DbSet<PhotoModels.ContentTag> ContentTags { get; set; }

        #endregion

        #region Social

        public DbSet<SocialModels.Like> Likes { get; set; }

        public DbSet<SocialModels.Comment> Comments { get; set; }

        #endregion
    }
}