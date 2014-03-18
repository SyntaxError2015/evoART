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
    public class UserAccountRepository : StandardRepository, IUserAccountRepository
    {
        private readonly DbSet<AccountModels.UserAccount> _dbSet;

        public UserAccountRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.UserAccounts;
        }

        public bool VerifyExists(string userName)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public bool Insert(AccountModels.UserAccount userAccount)
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

        public bool Delete(int userId)
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

        public bool Update(AccountModels.UserAccount userAccount)
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