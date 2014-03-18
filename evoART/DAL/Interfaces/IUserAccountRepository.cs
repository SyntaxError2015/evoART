using System;
using evoART.Models;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IUserAccountRepository : IDisposable
    {
        bool VerifyExists(string userName);

        bool VerifyPassword(string userName, string passWord);

        bool Insert(AccountModels.UserAccount userAccount);

        bool Delete(int userId);

        bool Update(AccountModels.UserAccount userAccount);
    }
}
