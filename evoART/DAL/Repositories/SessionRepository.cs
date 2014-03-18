using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;

namespace evoART.DAL.Repositories
{
    public class SessionRepository : StandardRepository, ISessions
    {
        private readonly DbSet<AccountModels.Session> _dbSet;

        public SessionRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.Sessions;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OpenSession(int userId)
        {
            throw new NotImplementedException();
        }

        public void CloseSession(int userId)
        {
            throw new NotImplementedException();
        }
    }
}