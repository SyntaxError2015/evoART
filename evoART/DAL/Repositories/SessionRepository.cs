using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;

namespace evoART.DAL.Repositories
{
    public class SessionRepository : StandardRepository, ISessions
    {
        public SessionRepository(DatabaseContext context) : base()
        {
        }

        public void Save()
        {
            throw new NotImplementedException();
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