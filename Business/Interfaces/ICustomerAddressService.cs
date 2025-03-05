using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerAddressService
    {
        Task<IEnumerable<CustomerAddress>> GetAllAsync();
        Task<CustomerAddress?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CustomerAddressForm form);
        Task<bool> UpdateAsync(int id, CustomerAddressForm form);
        Task<bool> DeleteAsync(int id);
    }
}
