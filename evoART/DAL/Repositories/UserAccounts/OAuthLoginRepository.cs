﻿using System;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.UserAccounts;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.UserAccounts
{
    public class OAuthLoginRepository : BaseRepository<AccountModels.OAuthLogin>, IOAuthLoginRepository
    {
        public OAuthLoginRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Verify if the entered provider name and id for the provider are correct for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        /// <param name="providerName">The name of the provider</param>
        /// <param name="idFromProvider">The Id (key) that the user has for that provider</param>
        /// <returns>A bool value indicating if the entered data is correct or not</returns>
        public bool VerifyDataCorresponds(string userName, string providerName, string idFromProvider)
        {
            try
            {
                return _dbSet.Count(p =>
                    p.UserAccount.UserName == userName &&
                    p.Provider == providerName &&
                    p.Key == idFromProvider) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify if there is already a registration existent for a combination { provider, key }
        /// </summary>
        /// <param name="providerName">The name of the provider</param>
        /// <param name="userId">The Id of the user</param>
        /// <returns>A bool value indicating if the entered data exist</returns>
        public bool VerifyExists(string providerName, Guid userId)
        {
            try
            {
                return _dbSet.Count(p =>
                    p.Provider == providerName &&
                    p.UserAccount.UserId == userId) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify if there is already a registration existent for a combination { provider, key }
        /// </summary>
        /// <param name="providerName">The name of the provider</param>
        /// <param name="idFromProvider">The Id that the user has for the entered provider</param>
        /// <returns>A bool value indicating if the entered data exist</returns>
        public bool VerifyExists(string providerName, string idFromProvider)
        {
            try
            {
                return _dbSet.Count(p =>
                    p.Provider == providerName &&
                    p.Key == idFromProvider) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the nickname of the user that has the specified external login details
        /// </summary>
        /// <param name="providerName">The name of the provider</param>
        /// <param name="idFromProvider">The Id that the user has for that provider</param>
        /// <returns>A String value</returns>
        public string GetUserNameForOAuth(string providerName, string idFromProvider)
        {
            try
            {
                return _dbSet.First(o => o.Provider == providerName && o.Key == idFromProvider).UserAccount.UserName;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Insert a new entry for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        /// <param name="providerName">The name of the provider</param>
        /// <param name="idFromProvider">The ID that the user has for that provider</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Insert(string userName, string providerName, string idFromProvider)
        {
            try
            {
                var item = new AccountModels.OAuthLogin()
                {
                    OAuthLoginId = Guid.NewGuid(),
                    UserAccount = _dbContext.UserAccounts.First(t => t.UserName == userName),
                    Provider = providerName,
                    Key = idFromProvider
                };

                _dbSet.Add(item);

                return Save();
            }

            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Delete an entry from the table
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        /// <param name="providerName">The name of the provider for which to delete the key</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Delete(string userName, string providerName)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(u => u.UserAccount.UserName == userName && u.Provider == providerName));

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}