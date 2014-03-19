using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private readonly DbSet<AccountModels.Role> _dbSet;

        public RoleRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.Roles;
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
                _dbSet.Remove(_dbSet.FirstOrDefault(r => r.RoleName == roleName));

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