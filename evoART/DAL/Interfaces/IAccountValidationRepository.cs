﻿using System;
using evoART.Models;

namespace evoART.DAL.Interfaces
{
    interface IAccountValidationRepository : IStandardInterface, IDisposable
    {
        string GetValidationToken(int userId);

        string GetValidationTokenExpireDate(int userId);

        int GetFailedLoginAttempts(int userId);

        void ResetLoginFailAttempts(int userId);

        void GenerateNewValidationToken(int userId);

        void SetVerified(int userId, bool verified);

        void Insert(AccountModels.AccountValidation validation, int userId);

        void Delete(int userId);

        void Update(AccountModels.UserAccount validation);
    }
}
