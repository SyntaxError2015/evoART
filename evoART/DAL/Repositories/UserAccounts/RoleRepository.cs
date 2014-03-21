using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.UserAccounts;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.UserAccounts
{
    public class RoleRepository : BaseRepository<AccountModels.Role>, IRoleRepository
    {
        public RoleRepository(UserAccountsContext context) : base(context)
        {
        }

        /// <summary>
        /// Get a string array with the names of all the roles
        /// </summary>
        public string[] GetRoleNames()
        {
            try
            {
                return  _dbSet.Select(s => s.RoleName).ToArray();
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get the Role entity which has the selected name
        /// </summary>
        /// <param name="roleName">The name of the role</param>
        /// <returns>A AccountModels.Role instance</returns>
        public AccountModels.Role GetRole(string roleName)
        {
            return _dbSet.First(r => r.RoleName == roleName);
        }

        /// <summary>
        /// Insert a new role in the database
        /// </summary>
        /// <returns>A bool value indicating the success of the operation</returns>
        public bool Insert(AccountModels.Role role)
        {
            try
            {
                _dbSet.Add(role);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a role from the database
        /// </summary>
        /// <returns>A bool value indicating the success of the operation</returns>
        public bool Delete(string roleName)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(r => r.RoleName == roleName));

                return Save();
            }
            
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Update role from the database
        /// </summary>
        /// <returns>A bool value indicating the success of the operation</returns>
        public bool Update(AccountModels.Role role)
        {
            try
            {
                _dbSet.AddOrUpdate(role);

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}