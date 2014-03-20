using System;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.UserAccounts;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.UserAccounts
{
    public class SessionRepository : BaseRepository<AccountModels.Session>, ISessionRepository
    {
        public SessionRepository(UserAccountsContext context) : base(context)
        {
        }

        /// <summary>
        /// Verify if a user is already logged in or not
        /// </summary>
        /// <param name="userName">The nickname of the user for which to verify</param>
        /// <returns>A bool value</returns>
        public bool IsLoggedIn(string userName)
        {
            try
            {
                return _dbSet.Count(u => u.UserAccount.UserName == userName) > 0;
            }

            catch
            {
                return false;
            }
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
                    SessionKey = Special.TokenGenerator.EncryptMD5(DateTime.Now.ToFileTime().ToString())
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