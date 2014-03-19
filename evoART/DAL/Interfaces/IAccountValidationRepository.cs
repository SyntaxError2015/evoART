using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IAccountValidationRepository
    {
        string GetValidationToken(string userName);

        DateTime GetValidationTokenExpireDate(string userName);

        int GetFailedLoginAttempts(string userName);

        void ResetLoginFailAttempts(string userName);

        bool GenerateNewValidationToken(string userName);

        bool GenerateNewValidationToken(AccountModels.AccountValidation validation);

        void SetAsVerified(string userName);

        bool CheckIfVerified(string userName);

        bool Insert(string userName);

        bool Delete(string userName);

        bool Update(AccountModels.AccountValidation validation);
    }
}
