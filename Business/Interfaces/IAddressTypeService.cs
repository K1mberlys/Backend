using Business.Models;

namespace Business.Interfaces
{
    public interface IAddressTypeService
    {
        Task<IEnumerable<AddressType>> GetAllAddressTypesAsync();
    }
}
