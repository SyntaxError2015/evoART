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
    public class AccountValidationRepository : StandardRepository, IAccountValidationRepository
    {
        private readonly DbSet<AccountModels.AccountValidation> _dbSet;

        public AccountValidationRepository(DatabaseContext context) : base(context)
        {
            _dbSet = context.AccountValidations;
        }

        public string GetValidationToken(int userId)
        {
            throw new NotImplementedException();
        }

        public string GetValidationTokenExpireDate(int userId)
        {
            throw new NotImplementedException();
        }

        public int GetFailedLoginAttempts(int userId)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginFailAttempts(int userId)
        {
            throw new NotImplementedException();
        }

        public void GenerateNewValidationToken(int userId)
        {
            throw new NotImplementedException();
        }

        public void SetVerified(int userId, bool verified)
        {
            throw new NotImplementedException();
        }

        public bool Insert(AccountModels.AccountValidation validation, int userId)
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

        public bool Update(AccountModels.UserAccount validation)
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