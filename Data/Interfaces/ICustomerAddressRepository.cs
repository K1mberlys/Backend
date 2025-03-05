using Data.Contexts;
using Data.Enteties;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Data.Interfaces;

public interface ICustomerAddressRepository : IBaseRepository<CustomerAddressEntity>
{
    Task<IEnumerable<CustomerAddressEntity>> GetByCustomerIdAsync(int customerId);
}

