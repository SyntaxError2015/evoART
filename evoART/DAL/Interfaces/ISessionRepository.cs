namespace evoART.DAL.Interfaces
{
    interface ISessionRepository
    {
        bool Login(int userId);

        bool Logout(int userId);
    }
}
