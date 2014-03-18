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

        public UserAccountRepository(DatabaseContext context)
            : base(context)
        {
            _dbSet = context.UserAccounts;
        }

        /// <summary>
        /// Verify if the entered user already exists in the database
        /// </summary>
        /// <param name="userName">The name of the user</param>
        /// <returns>A bool value telling if the user exists or not</returns>
        public bool VerifyExists(string userName)
        {
            try
            {
                return _dbSet.Count(u => u.UserName == userName) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify if the password is correct for a certain user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool VerifyPassword(string userName, string password)
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