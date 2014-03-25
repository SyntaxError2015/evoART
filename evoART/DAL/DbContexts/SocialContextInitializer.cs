using System.Data.Entity;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public static class SocialContextInitializer
    {
        public static void InitializeSocialModels(DbModelBuilder modelBuilder)
        {
            InitializeLikesTable(modelBuilder);
            InitializeCommentsTable(modelBuilder);
        }

        private static void InitializeLikesTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialModels.Like>()
                .HasKey(k => k.LikeId);

            modelBuilder.Entity<SocialModels.Like>()
                .Property(p => p.LikeDate).IsRequired();

            // Map foreign key to the Photos table
            modelBuilder.Entity<SocialModels.Like>()
                .HasRequired(p => p.Photo)
                .WithMany(p => p.Likes)
                .Map(m => m.MapKey("PhotoId"))
                .WillCascadeOnDelete(false);

            // Map foreign key to the UserAccounts table
            modelBuilder.Entity<SocialModels.Like>()
                .HasRequired(u => u.UserAccount)
                .WithMany(u => u.Likes)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(false);
        }

        private static void InitializeCommentsTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SocialModels.Comment>()
                .HasKey(k => k.CommentId);

            modelBuilder.Entity<SocialModels.Comment>()
                .Property(p => p.CommentText).IsRequired();

            modelBuilder.Entity<SocialModels.Comment>()
                .Property(p => p.CommentDate).IsRequired();

            // Map foreign key to the Photos table
            modelBuilder.Entity<SocialModels.Comment>()
                .HasRequired(p => p.Photo)
                .WithMany(p => p.Comments)
                .Map(m => m.MapKey("PhotoId"))
                .WillCascadeOnDelete(false);

            // Map foreign key to the UserAccounts table
            modelBuilder.Entity<SocialModels.Comment>()
                .HasRequired(u => u.UserAccount)
                .WithMany(u => u.Comments)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(false);
        }
    }
}