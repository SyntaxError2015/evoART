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
    public class UserAccountRepository : StandardRepository, IUserAccountRepository
    {
        private readonly DbSet<AccountModels.UserAccount> _dbSet;

        public UserAccountRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.UserAccounts;
        }

        public void Dispose()
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