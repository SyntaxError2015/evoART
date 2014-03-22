﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public static class PhotosContextInitializer
    {
        public static void InitializePhotosModels(DbModelBuilder modelBuilder)
        {
            InitializePhotosTable(modelBuilder);
            InitializeAlbumsTable(modelBuilder);
            InitializeHashTagsTable(modelBuilder);
        }

        private static void InitializePhotosTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Photo>()
                .HasKey(k => k.PhotoId);

            modelBuilder.Entity<PhotoModels.Photo>()
                .Property(p => p.PhotoName).IsOptional();

            modelBuilder.Entity<PhotoModels.Photo>()
                .Property(p => p.PhotoDescription).IsOptional();

            modelBuilder.Entity<PhotoModels.Photo>()
                .Property(p => p.UploadDate).IsRequired();

            // Map foreign key to the Users table
            modelBuilder.Entity<PhotoModels.Photo>()
                .HasRequired(u => u.UserAccount)
                .WithMany(u => u.Photos)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            // Map foreign key to the Albums table
            modelBuilder.Entity<PhotoModels.Photo>()
                .HasRequired(a => a.Album)
                .WithMany(a => a.Photos)
                .Map(m => m.MapKey("AlbumId"))
                .WillCascadeOnDelete(false);

            // Map the many-to-many relationship between the Photos table and the HashTags table
            modelBuilder.Entity<PhotoModels.Photo>()
                .HasMany(hp => hp.HashTags)
                .WithMany(hp => hp.Photos)
                .Map(m =>
                {
                    m.ToTable("HashTags_Photos");
                    m.MapLeftKey("HashTagId");
                    m.MapRightKey("PhotoId");
                });
        }

        private static void InitializeAlbumsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Album>()
                .HasKey(k => k.AlbumId);

            // Map foreign key to the UserAccounts table
            modelBuilder.Entity<PhotoModels.Album>()
                .HasRequired(u => u.UserAccount)
                .WithMany(u => u.Albums)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PhotoModels.Album>()
                .Property(p => p.AlbumName).IsRequired();
            
            modelBuilder.Entity<PhotoModels.Album>()
                .Property(p => p.AlbumDescription).IsOptional();
        }

        private static void InitializeHashTagsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.HashTag>()
                .HasKey(k => k.HashTagId);

            modelBuilder.Entity<PhotoModels.HashTag>()
                .Property(p => p.HashTagName).IsRequired();
        }
    }
}