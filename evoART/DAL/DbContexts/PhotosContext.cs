using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public class PhotosContext : DbContext
    {
        protected internal PhotosContext()
            : base("evoARTPhotos")
        {
            Database.SetInitializer(new PhotosInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            InitializePhotosTable(modelBuilder);
            InitializeAlbumsTable(modelBuilder);
            InitializeCategoriesTable(modelBuilder);
            InitializeKeywordsTable(modelBuilder);
            InitializeKeywordPhotosTable(modelBuilder);
            InitializePhotoCategoriesTable(modelBuilder);
        }

        public void InitializePhotosTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Photo>()
                .HasKey(k => k.PhotoId)
                .Property(p => p.PhotoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<PhotoModels.Photo>()
                .Property(p => p.Name).IsOptional();

            modelBuilder.Entity<PhotoModels.Photo>()
                .Property(p => p.Description).IsOptional();

            modelBuilder.Entity<PhotoModels.Photo>()
                .Property(p => p.UploadDate).IsRequired();

            modelBuilder.Entity<PhotoModels.Photo>()
                .HasRequired(u => u.UserAccount)
                .WithMany(u => u.Photos)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PhotoModels.Photo>()
                .HasRequired(a => a.Album)
                .WithMany(a => a.Photos)
                .Map(m => m.MapKey("AlbumId"))
                .WillCascadeOnDelete(true);
        }

        public void InitializeAlbumsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Album>()
                .HasKey(k => k.AlbumId);
        }

        public void InitializeCategoriesTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Category>()
                .HasKey(k => k.CategoryId);
        }

        public void InitializeKeywordsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Keyword>()
                .HasKey(k => k.KeywordId);
        }

        public void InitializePhotoCategoriesTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Photo_Category>()
                .HasKey(k => k.Photo_CategoryId);
        }

        public void InitializeKeywordPhotosTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Keyword_Photo>()
                .HasKey(k => k.Keyword_PhotoId);
        }

        public DbSet<PhotoModels.Photo> Photos { get; set; }

        public DbSet<PhotoModels.Album> Albums { get; set; }

        public DbSet<PhotoModels.Category> Categories { get; set; }

        public DbSet<PhotoModels.Keyword> Keywords { get; set; }

        public DbSet<PhotoModels.Photo_Category> PhotoCategories { get; set; }

        public DbSet<PhotoModels.Keyword_Photo> KeywordPhotos { get; set; }
    }
}