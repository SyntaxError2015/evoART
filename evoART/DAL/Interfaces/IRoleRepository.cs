using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IRoleRepository
    {
        string[] GetRoleNames();

        AccountModels.Role GetRole(string roleName);

        bool Insert(AccountModels.Role role);

        bool Delete(string roleName);

        bool Update(AccountModels.Role role);
    }
}
