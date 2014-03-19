namespace evoART.DAL.Interfaces
{
    interface ISessionRepository
    {
        bool Login(string userName);

        bool Logout(string userName);
    }
}
