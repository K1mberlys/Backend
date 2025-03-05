using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRolesRepository(DataContext context) : BaseRepository<UserRolesEntity>(context), IUserRolesRepository
{
    public async Task<IEnumerable<UserRolesEntity>> GetRolesByUserIdAsync(int userId)
    {
        return await _context.UserRoles
            .Include(x => x.Role)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<UserRolesEntity?> GetByIdsAsync(int userId, int roleId)
    {
        return await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }

}
