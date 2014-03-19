﻿namespace evoART.DAL.Interfaces
{
    internal interface IOAuthLoginRepository
    {
        bool VerifyDataCorresponds(string userName, string providerName, string idFromProvider);

        bool VerifyExists(string providerName, string idFromProvider);

        bool Insert(string userName, string providerName, string idFromProvider);

        bool Delete(string userName, string providerName);
    }
}