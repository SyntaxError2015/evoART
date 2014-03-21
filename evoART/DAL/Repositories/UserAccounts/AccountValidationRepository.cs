using System;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.UserAccounts;
using evoART.Models.DbModels;
using evoART.Special;

namespace evoART.DAL.Repositories.UserAccounts
{
    public class AccountValidationRepository : BaseRepository<AccountModels.AccountValidation>, IAccountValidationRepository
    {
        public AccountValidationRepository(DatabaseContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get the validation token for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        /// <returns>A string value</returns>
        public string GetValidationToken(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            return validation == null ? null : validation.ValidationToken;
        }

        /// <summary>
        /// Get the date and time when the validation token expires
        /// </summary>
        /// <param name="userName">The nickname of the user for which to get the data</param>
        /// <returns>A DateTime instance</returns>
        public DateTime GetValidationTokenExpireDate(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            return validation == null ? DateTime.Now : validation.ValidationTokenExpireDate;
        }

        /// <summary>
        /// Get the number of failed login attempts for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        /// <returns>An integer value</returns>
        public int GetFailedLoginAttempts(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            return validation == null ? -1 : validation.LoginFails;
        }

        /// <summary>
        /// Increment the number of login fail attempts for a certain user
        /// </summary>
        /// <param name="userName">The user for which to do the incrementation</param>
        public void IncrementFailedLoginAttempts(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            validation.LoginFails++;

            Update(validation);
        }

        /// <summary>
        /// Reset the number of login attempts to 0 for a user
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        public void ResetLoginFailAttempts(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            if (validation != null)
                Update(validation);
        }

        /// <summary>
        /// Generates a new validation token for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        public bool GenerateNewValidationToken(string userName)
        {
            return (GenerateNewValidationToken(_dbSet.First(v => v.UserAccount.UserName == userName)));
        }

        /// <summary>
        /// Generates a new validation token for a certain user
        /// </summary>
        /// <param name="validation">The validation entity for which to update the token</param>
        /// <returns></returns>
        public bool GenerateNewValidationToken(AccountModels.AccountValidation validation)
        {
            validation.ValidationToken = TokenGenerator.GenerateValidationToken(validation);
            validation.ValidationTokenExpireDate = DateTime.Now.AddHours(24);

            return Update(validation);
        }

        /// <summary>
        /// Set a certain user account as being verified
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        public void SetAsVerified(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            if (validation == null)
                return;

            validation.IsVerified = true;

            Update(validation);
        }

        /// <summary>
        /// Set a certain user account as NOT being verified
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        public void SetAsNotVerified(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            if (validation == null)
                return;

            validation.IsVerified = false;

            Update(validation);
        }

        /// <summary>
        /// Check if a certain user account is verified
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        public bool CheckIfVerified(string userName)
        {
            var validation = _dbSet.First(t => t.UserAccount.UserName == userName);

            return validation != null && validation.IsVerified;
        }

        /// <summary>
        /// Insert a new account validation instance into the database
        /// </summary>
        /// <param name="userName">The nickname of the user for which to se the new validation</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Insert(string userName)
        {
            try
            {
                var validation = new AccountModels.AccountValidation
                {
                    UserAccount = _dbContext.UserAccounts.First(u => u.UserName == userName),
                    ValidationTokenExpireDate = DateTime.Now.AddHours(24),
                    LoginFails = 0,
                    IsVerified = false
                };

                validation.ValidationToken = TokenGenerator.GenerateValidationToken(validation);

                _dbSet.Add(validation);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete an account validation instance from the database
        /// </summary>
        /// <param name="userName">The nickname of the user for which to se the new validation</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Delete(string userName)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(r => r.UserAccount.UserName == userName));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Update a certain account validation entity
        /// </summary>
        /// <param name="validation">The AccountValidation instance to update</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Update(AccountModels.AccountValidation validation)
        {
            try
            {
                _dbSet.AddOrUpdate(validation);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        
    }
}