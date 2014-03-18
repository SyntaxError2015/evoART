using System;
using evoART.Models;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IAccountValidationRepository : IDisposable
    {
        string GetValidationToken(int userId);

        string GetValidationTokenExpireDate(int userId);

        int GetFailedLoginAttempts(int userId);

        void ResetLoginFailAttempts(int userId);

        void GenerateNewValidationToken(int userId);

        void SetVerified(int userId, bool verified);

        bool Insert(AccountModels.AccountValidation validation, int userId);

        bool Delete(int userId);

        bool Update(AccountModels.UserAccount validation);
    }
}
