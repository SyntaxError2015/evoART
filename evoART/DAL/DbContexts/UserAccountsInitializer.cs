using System.Data.Entity;
using System.Linq;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public class UserAccountsInitializer : DropCreateDatabaseIfModelChanges<UserAccountsContext>
    {
        public override void InitializeDatabase(UserAccountsContext context)
        {
            base.InitializeDatabase(context);

            PopulateRolesTable();
        }

        private static void PopulateRolesTable()
        {
            using (var db = new UserAccountsContext())
            {
                if (db.Roles.Any()) return;

                UserAccountsWorkUnit.Instance.RoleRepository.Insert(new AccountModels.Role { RoleName = "Simple user" });
                UserAccountsWorkUnit.Instance.RoleRepository.Insert(new AccountModels.Role { RoleName = "Photographer" });
            }
        }
    }
}