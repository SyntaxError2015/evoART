using System;
namespace evoART.DAL.Interfaces.UserAccounts
{
    internal interface IOAuthLoginRepository
    {
        bool VerifyDataCorresponds(string userName, string providerName, string idFromProvider);

        bool VerifyExists(string providerName, Guid userId);

        string GetUserNameForOAuth(string providerName, string idFromProvider);

        bool Insert(string userName, string providerName, string idFromProvider);

        bool Delete(string userName, string providerName);
    }
}
