using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IRoleRepository
    {
        string[] GetRoleNames();

        bool Insert(AccountModels.Role role);

        bool Delete(string roleName);

        bool Update(AccountModels.Role role);
    }
}
