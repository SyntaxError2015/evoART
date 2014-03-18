using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;

namespace evoART.DAL.Repositories
{
    public class UserAccountRepository : StandardRepository, IUserAccountRepository
    {
        protected internal UserAccountRepository(DatabaseContext context) : base(context)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}