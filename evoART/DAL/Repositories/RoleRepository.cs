using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;
using evoART.Models.DbModels;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

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
        public bool Delete(int roleId)
        {
            try
            {
                _dbSet.Remove(_dbSet.Find(roleId));

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