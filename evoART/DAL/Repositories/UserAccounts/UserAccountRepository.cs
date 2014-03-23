using System;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.UserAccounts;
using evoART.DAL.UnitsOfWork;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.UserAccounts
{
    public class UserAccountRepository : BaseRepository<AccountModels.UserAccount>, IUserAccountRepository
    {
        private const string DEFAULT_ALBUM_NAME = "My album";

        public UserAccountRepository(DatabaseContext context)
            : base(context)
        {
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
        /// <param name="userName">The name of the user</param>
        /// <param name="password">The entered password to verify</param>
        /// <returns></returns>
        public bool VerifyPassword(string userName, string password)
        {
            try
            {
                password = Special.TokenGenerator.EncryptMD5(password);

                return _dbSet.Count(u => u.UserName == userName && u.Password == password) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insert a new user into the database.
        /// The password for the user if encrypted with MD5 before adding the user to the database.
        /// This also creates an entry in the AccountValidation table and a default album for the user
        /// </summary>
        /// <returns>A bool value indicating if the operation was successful</returns>
        public bool Insert(AccountModels.UserAccount userAccount)
        {
            try
            {
                userAccount.UserId = Guid.NewGuid();
                userAccount.Password = Special.TokenGenerator.EncryptMD5(userAccount.Password);
                
                _dbSet.Add(userAccount);

                //return Save() && 
                //    DatabaseWorkUnit.Instance.AccountValidationRepository.Insert(userAccount.UserName)
                //    && DatabaseWorkUnit.Instance.AlbumsRepository.Insert(DEFAULT_ALBUM_NAME);

                bool ok = Save();

                if (ok)
                    ok = DatabaseWorkUnit.Instance.AccountValidationRepository.Insert(userAccount.UserName);

                if (ok)
                    ok = DatabaseWorkUnit.Instance.AlbumsRepository.Insert(userAccount.UserId, DEFAULT_ALBUM_NAME);

                return ok;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a user from the database
        /// </summary>
        /// <returns>A bool value indicating if the operation was successful</returns>
        public bool Delete(string userName)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(u => u.UserName == userName));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Update the details for a certain user
        /// </summary>
        /// <returns>A bool value indicating if the operation was successful</returns>
        public bool Update(AccountModels.UserAccount userAccount)
        {
            try
            {
                _dbSet.AddOrUpdate(userAccount);

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}