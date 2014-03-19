using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories
{
    public class AccountValidationRepository : BaseRepository, IAccountValidationRepository
    {
        private readonly DbSet<AccountModels.AccountValidation> _dbSet;

        public AccountValidationRepository(DatabaseContext context)
            : base(context)
        {
            _dbSet = context.AccountValidations;
        }

        /// <summary>
        /// Get the validation token for a certain user
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>A string value</returns>
        public string GetValidationToken(int userId)
        {
            var validation = _dbSet.FirstOrDefault(t => t.UserAccount.UserId == userId);

            return validation == null ? null : validation.ValidationToken;
        }

        /// <summary>
        /// Get the date and time when the validation token expires
        /// </summary>
        /// <param name="userId">The Id of the user for which to get the data</param>
        /// <returns>A DateTime instance</returns>
        public DateTime GetValidationTokenExpireDate(int userId)
        {
            var validation = _dbSet.FirstOrDefault(t => t.UserAccount.UserId == userId);

            return validation == null ? DateTime.Now : validation.ValidationTokenExpireDate;
        }

        /// <summary>
        /// Get the number of failed login attempts for a certain user
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns>An integer value</returns>
        public int GetFailedLoginAttempts(int userId)
        {
            var validation = _dbSet.FirstOrDefault(t => t.UserAccount.UserId == userId);

            return validation == null ? -1 : validation.LoginFails;
        }

        /// <summary>
        /// Reset the number of login attempts to 0 for a user
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        public void ResetLoginFailAttempts(int userId)
        {
            var validation = _dbSet.FirstOrDefault(t => t.UserAccount.UserId == userId);

            if (validation != null)
                Update(validation);
        }

        /// <summary>
        /// Generates a new validation token for a certain user
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        public bool GenerateNewValidationToken(int userId)
        {
            return (GenerateNewValidationToken(_dbSet.FirstOrDefault(v => v.UserAccount.UserId == userId)));
        }

        /// <summary>
        /// Generates a new validation token for a certain user
        /// </summary>
        /// <param name="validation">The validation entity for which to update the token</param>
        /// <returns></returns>
        public bool GenerateNewValidationToken(AccountModels.AccountValidation validation)
        {
            validation.ValidationToken = GetNewValidationToken(validation);
            validation.ValidationTokenExpireDate = DateTime.Now.AddHours(24);

            return Update(validation);
        }

        /// <summary>
        /// Set a certain user account as being verified
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        public void SetAsVerified(int userId)
        {
            var validation = _dbSet.FirstOrDefault(t => t.UserAccount.UserId == userId);

            if (validation == null)
                return;

            validation.IsVerified = true;

            Update(validation);
        }

        /// <summary>
        /// Insert a new account validation instance into the database
        /// </summary>
        /// <param name="userId">The Id of the user for which to se the new validation</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Insert(int userId)
        {
            try
            {
                var validation = new AccountModels.AccountValidation
                {
                    UserAccount = _dbContext.UserAccounts.Find(userId),
                    ValidationTokenExpireDate = DateTime.Now.AddHours(24),
                    LoginFails = 0,
                    IsVerified = false
                };

                validation.ValidationToken = GetNewValidationToken(validation);

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
        /// <param name="userId">The Id of the user for which to se the new validation</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Delete(int userId)
        {
            try
            {
                _dbSet.Remove(_dbSet.FirstOrDefault(r => r.UserAccount.UserId == userId));

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

        private static string GetNewValidationToken(AccountModels.AccountValidation validation)
        {
            return validation.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }
    }
}