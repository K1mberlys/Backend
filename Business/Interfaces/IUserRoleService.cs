using Business.Models;

namespace Business.Interfaces;

public interface IUserRoleService
{
    Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
    Task<bool> AssignRoleToUserAsync(int userId, int roleId);
    Task<bool> RemoveUserRoleAsync(int userId, int roleId);
}
