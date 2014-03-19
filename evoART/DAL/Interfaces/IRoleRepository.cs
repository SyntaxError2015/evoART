using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces
{
    interface IRoleRepository
    {
        string[] GetRoleNames();

        bool Insert(AccountModels.Role role);

        bool Delete(int roleId);

        bool Update(AccountModels.Role role);
    }
}
