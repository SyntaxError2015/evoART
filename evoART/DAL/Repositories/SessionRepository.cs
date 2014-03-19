using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories
{
    public class SessionRepository : BaseRepository, ISessions
    {
        private readonly DbSet<AccountModels.Session> _dbSet;

        public SessionRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.Sessions;
        }

        /// <summary>
        /// Open a session for a certain user
        /// </summary>
        /// <param name="userId">The Id of the user who wants to log in</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Login(int userId)
        {
            try
            {
                var session = new AccountModels.Session
                {
                    UserAccount = _dbContext.UserAccounts.Find(userId),
                    SessionKey = MD5.Create(DateTime.Now.ToFileTime().ToString()).GetHashCode().ToString()
                };

                _dbSet.Add(session);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Close a session for a certain user
        /// </summary>
        /// <param name="userId">The Id of the user who wants to log out</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Logout(int userId)
        {
            try
            {
                _dbSet.Remove(_dbSet.FirstOrDefault(t => t.UserAccount.UserId == userId));

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}