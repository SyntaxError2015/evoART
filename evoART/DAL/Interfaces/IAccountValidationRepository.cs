using System;
using evoART.Models;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IAccountValidationRepository
    {
        string GetValidationToken(int userId);

        DateTime GetValidationTokenExpireDate(int userId);

        int GetFailedLoginAttempts(int userId);

        void ResetLoginFailAttempts(int userId);

        bool GenerateNewValidationToken(int userId);

        bool GenerateNewValidationToken(AccountModels.AccountValidation validation);

        void SetAsVerified(int userId);

        bool Insert(int userId);

        bool Delete(int userId);

        bool Update(AccountModels.AccountValidation validation);
    }
}
