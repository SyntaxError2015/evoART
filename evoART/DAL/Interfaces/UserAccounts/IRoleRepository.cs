using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.UserAccounts
{
    interface IRoleRepository
    {
        string[] GetRoleNames();

        AccountModels.Role GetRole(string roleName);

        bool Insert(string roleName);

        bool Delete(string roleName);

        bool Update(AccountModels.Role role);
    }
}
