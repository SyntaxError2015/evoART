using System;
using evoART.Models;

namespace evoART.DAL.Interfaces
{
    public interface IUserAccountRepository : IStandardInterface, IDisposable
    {
        bool VerifyExists(string userName);

        bool VerifyPassword(string userName, string passWord);

        void Insert(AccountModels.UserAccount userAccount);
        
        void Delete(int userId);

        void Update(AccountModels.UserAccount userAccount);
    }
}
