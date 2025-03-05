using Data.Enteties;
using Microsoft.EntityFrameworkCore;

namespace Data.Interfaces;

public interface IUserRolesRepository : IBaseRepository<UserRolesEntity>
{
    Task<IEnumerable<UserRolesEntity>> GetRolesByUserIdAsync(int userId);
    Task<UserRolesEntity?> GetByIdsAsync(int userId, int roleId);
}