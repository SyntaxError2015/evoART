using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.UserAccounts
{
    interface ISessionRepository
    {
        AccountModels.UserAccount GetUser(int sessionId, string sessionKey);

        AccountModels.Session Login(string userName);

        bool Logout(string userName);
    }
}
