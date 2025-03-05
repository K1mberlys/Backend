using Business.Models;

namespace Business.Interfaces
{
    public interface IUserAddressService
    {
        Task<IEnumerable<UserAddress>> GetAllAsync();
        Task<UserAddress?> GetByIdAsync(int id);
        Task<bool> CreateAsync(UserAddressForm form);
        Task<bool> UpdateAsync(int id, UserAddressForm form);
        Task<bool> DeleteAsync(int id);
    }
}