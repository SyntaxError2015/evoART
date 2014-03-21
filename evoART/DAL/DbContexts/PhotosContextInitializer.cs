using System.ComponentModel.DataAnnotations.Schema;
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
            InitializeCategoriesTable(modelBuilder);
            InitializeKeywordsTable(modelBuilder);
        }

        private static void InitializePhotosTable(DbModelBuilder modelBuilder)
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

            // Map the many-to-many relationship between the Photos table and the Categories table
            modelBuilder.Entity<PhotoModels.Photo>()
                .HasMany(pc => pc.Categories)
                .WithMany(pc => pc.Photos)
                .Map(m =>
                {
                    m.ToTable("Photos_Categories");
                    m.MapLeftKey("CategoryId");
                    m.MapRightKey("PhotoId");
                });

            // Map the many-to-many relationship between the Photos table and the Keywords table
            modelBuilder.Entity<PhotoModels.Photo>()
                .HasMany(kp => kp.Keywords)
                .WithMany(kp => kp.Photos)
                .Map(m =>
                {
                    m.ToTable("Keywords_Photos");
                    m.MapLeftKey("KeywordId");
                    m.MapRightKey("PhotoId");
                });
        }

        private static void InitializeAlbumsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Album>()
                .HasKey(k => k.AlbumId)
                .Property(k => k.AlbumId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<PhotoModels.Album>()
                .Property(p => p.AlbumName).IsRequired();
            
            modelBuilder.Entity<PhotoModels.Album>()
                .Property(p => p.AlbumDescription).IsOptional();
        }

        private static void InitializeCategoriesTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Category>()
                .HasKey(k => k.CategoryId)
                .Property(k => k.CategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<PhotoModels.Category>()
                .Property(p => p.CategoryName).IsRequired();

            modelBuilder.Entity<PhotoModels.Category>()
                .Property(p => p.CategoryDescription).IsOptional();
        }

        private static void InitializeKeywordsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoModels.Keyword>()
                .HasKey(k => k.KeywordId)
                .Property(k => k.KeywordId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<PhotoModels.Keyword>()
                .Property(p => p.KeywordName).IsRequired();
        }
    }
}