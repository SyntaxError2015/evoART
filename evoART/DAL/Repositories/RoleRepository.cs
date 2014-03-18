using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;
using Microsoft.Ajax.Utilities;

namespace evoART.DAL.Repositories
{
    public class RoleRepository : StandardRepository, IRoleRepository
    {
        private readonly DbSet<AccountModels.Role> _dbSet;

        public RoleRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.Roles;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string[] GetRoleNames()
        {
            throw new NotImplementedException();
        }

        public void Insert(AccountModels.Role role)
        {
            _dbSet.Add(role);
        }

        public void Delete(int roleId)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountModels.Role role)
        {
            throw new NotImplementedException();
        }
    }
}