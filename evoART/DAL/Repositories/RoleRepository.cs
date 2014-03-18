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
    }
}