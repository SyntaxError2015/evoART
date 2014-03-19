﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories
{
    public class SessionRepository : BaseRepository<AccountModels.Session>, ISessionRepository
    {
        public SessionRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Open a session for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user who wants to log in</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Login(string userName)
        {
            try
            {
                var session = new AccountModels.Session
                {
                    UserAccount = _dbContext.UserAccounts.FirstOrDefault(u => u.UserName == userName),
                    SessionKey = Special.TextWarping.EncryptMD5(DateTime.Now.ToFileTime().ToString())
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
        /// <param name="userName">The nickname of the user who wants to log out</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Logout(string userName)
        {
            try
            {
                _dbSet.Remove(_dbSet.FirstOrDefault(t => t.UserAccount.UserName == userName));

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}