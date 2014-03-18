using System;
using System.Collections.Generic;
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
        protected internal RoleRepository(DatabaseContext context) : base()
        {
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
            DbContext.Roles.Add(role);
        }

        public void Delete(int roleId)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountModels.Role role)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}