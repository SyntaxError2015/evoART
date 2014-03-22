using System;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.UserAccounts;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.UserAccounts
{
    public class SessionRepository : BaseRepository<AccountModels.Session>, ISessionRepository
    {
        public SessionRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Get the user that has the selected session details
        /// </summary>
        /// <param name="sessionId">The Id of the session</param>
        /// <param name="sessionKey">The Key for the session</param>
        /// <returns>An UserAccount instance or null if there is no session for the user</returns>
        public AccountModels.UserAccount GetUser(int sessionId, string sessionKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Open a session for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user who wants to log in</param>
        /// <returns>A Session instance or a null value if it doesn't exist</returns>
        public AccountModels.Session Login(string userName)
        {
            try
            {
                var session = new AccountModels.Session
                {
                    UserAccount = _dbContext.UserAccounts.First(u => u.UserName == userName),
                    SessionKey = Special.TokenGenerator.EncryptMD5(DateTime.Now.ToFileTime().ToString())
                };

                _dbSet.Add(session);

                return Save() ? session : null;
            }

            catch
            {
                return null;
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
                _dbSet.Remove(_dbSet.First(t => t.UserAccount.UserName == userName));

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}