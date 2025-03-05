using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerAddressRepository(DataContext context) : BaseRepository<CustomerAddressEntity>(context), ICustomerAddressRepository
{
    public async Task<IEnumerable<CustomerAddressEntity>> GetByCustomerIdAsync(int customerId)
    {
        return await _context.CustomersAddresses
            .Include(x => x.Customer)
            .Include(x => x.AddressType)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }
}

