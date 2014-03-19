using System.Data.Entity;
using System.Linq;
using evoART.DAL.UnitOfWork;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);

            PopulateRolesTable();
        }

        private static void PopulateRolesTable()
        {
            using (var db = new DatabaseContext())
            {
                if (db.Roles.Any()) return;

                DatabaseWorkUnit.Instance.RoleRepository.Insert(new AccountModels.Role { RoleName = "Simple user" });
                DatabaseWorkUnit.Instance.RoleRepository.Insert(new AccountModels.Role { RoleName = "Photographer" });
            }
        }
    }
}