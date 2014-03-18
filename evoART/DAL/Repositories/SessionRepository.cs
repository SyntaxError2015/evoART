using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories
{
    public class SessionRepository : StandardRepository, ISessions
    {
        private readonly DbSet<AccountModels.Session> _dbSet;

        public SessionRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.Sessions;
        }

        public bool OpenSession(int userId)
        {
            try
            {
                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool CloseSession(int userId)
        {
            try
            {
                return true;
            }

            catch
            {
                return false;
            }
        }
    }
}