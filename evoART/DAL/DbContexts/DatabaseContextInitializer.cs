using System.Data.Entity;
using System.Linq;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;

namespace evoART.DAL.DbContexts
{
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);

            PopulateRolesTable();
            PopulateAlbumsTable();
        }

        private static void PopulateRolesTable()
        {
            using (var db = new DatabaseContext())
            {
                DatabaseWorkUnit.Instance.RoleRepository.Insert("Simple user" );
                DatabaseWorkUnit.Instance.RoleRepository.Insert("Photographer" );
            }
        }

        private static void PopulateAlbumsTable()
        {
            // Create a default item for the albums table
        }
    }
}