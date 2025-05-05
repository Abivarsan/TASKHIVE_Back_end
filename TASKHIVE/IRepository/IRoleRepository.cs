
using TASKHIVE.Model;

namespace TASKHIVE.IRepository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task update(Role role);
        Task<Role> GetRoleByUserRole(UserRole userRole);
    }
}
