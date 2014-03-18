using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;

namespace evoART.DAL.Repositories
{
    public class RoleRepository : StandardRepository, IRoleRepository
    {
        protected internal RoleRepository(DatabaseContext context) : base(context)
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
            throw new NotImplementedException();
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