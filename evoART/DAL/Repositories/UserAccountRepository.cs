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
        protected internal UserAccountRepository(DatabaseContext context) : base()
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool VerifyExists(string userName)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public void Insert(AccountModels.UserAccount userAccount)
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountModels.UserAccount userAccount)
        {
            throw new NotImplementedException();
        }
    }
}