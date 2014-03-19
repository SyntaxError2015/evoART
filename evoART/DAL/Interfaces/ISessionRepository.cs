namespace evoART.DAL.Interfaces
{
    interface ISessionRepository
    {
        bool IsLoggedIn(string userName);

        bool Login(string userName);

        bool Logout(string userName);
    }
}
