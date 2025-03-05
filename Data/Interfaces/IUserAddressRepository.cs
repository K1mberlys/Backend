using Data.Enteties;

namespace Data.Interfaces;

public interface IUserAddressRepository : IBaseRepository<UserAddressEntity>
{
    Task<IEnumerable<UserAddressEntity>> GetByUserIdAsync(int userId);
}
