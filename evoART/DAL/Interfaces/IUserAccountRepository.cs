using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IUserAccountRepository
    {
        bool VerifyExists(string userName);

        bool VerifyPassword(string userName, string password);

        bool Insert(AccountModels.UserAccount userAccount);

        bool Delete(int userId);

        bool Update(AccountModels.UserAccount userAccount);
    }
}
