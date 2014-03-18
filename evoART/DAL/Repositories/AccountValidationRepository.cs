using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;

namespace evoART.DAL.Repositories
{
    public class AccountValidationRepository : StandardRepository, IAccountValidationRepository
    {
        protected internal AccountValidationRepository(DatabaseContext context) : base(context)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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

        public void Insert(AccountModels.AccountValidation validation, int userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountModels.UserAccount validation)
        {
            throw new NotImplementedException();
        }
    }
}