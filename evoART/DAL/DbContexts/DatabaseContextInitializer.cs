using System;
using System.Data.Entity;
using System.Linq;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public class DatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);

            PopulateRolesTable();
            PopulateContentTagsTable();
        }

        private static void PopulateRolesTable()
        {
            using (var db = new DatabaseContext())
            {
                if (db.Roles.Any()) return;

                db.Roles.Add(new AccountModels.Role {RoleId = Guid.NewGuid(), RoleName = "Simple user"});
                db.Roles.Add(new AccountModels.Role {RoleId = Guid.NewGuid(), RoleName = "Photographer" });
            }
        }

        private static void PopulateContentTagsTable()
        {
            using (var db = new DatabaseContext())
            {
                if (db.ContentTags.Any()) return;

                db.ContentTags.Add(new PhotoModels.ContentTag
                {
                    ContentTagId = Guid.NewGuid(),
                    ContentTagName = "SFW",
                    ContentTagDescription = "Safe for work - has no mature content"
                });

                db.ContentTags.Add(new PhotoModels.ContentTag
                {
                    ContentTagId = Guid.NewGuid(),
                    ContentTagName = "SEXY",
                    ContentTagDescription = "Has mild mature content which may not be appropriate for children"
                });

                db.ContentTags.Add(new PhotoModels.ContentTag
                {
                    ContentTagId = Guid.NewGuid(),
                    ContentTagName = "NSFW",
                    ContentTagDescription = "NOT safe for work - has explicit mature content (not appropriate for children)"
                });
            }
        }
    }
}