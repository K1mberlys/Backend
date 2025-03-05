using Business.Models;

namespace Business.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<bool> CreateRoleAsync(RoleForm form);
    Task<bool> UpdateRoleAsync(int id, RoleForm form);
    Task<bool> RemoveRoleAsync(int roleId);
}
