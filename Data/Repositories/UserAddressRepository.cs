using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserAddressRepository(DataContext context) : BaseRepository<UserAddressEntity>(context), IUserAddressRepository
{
    public async Task<IEnumerable<UserAddressEntity>> GetByUserIdAsync(int userId)
    {
        return await _context.UserAddresses
            .Include(x => x.User)
            .Include(x => x.AddressType)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}
