using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class InvoiceRepository(DataContext context) : BaseRepository<InvoiceEntity>(context), IInvoiceRepository
{
    public async Task<IEnumerable<InvoiceEntity>> GetInvoicesWithProjectsAsync() 
    {
        return await _context.Invoices
            .Include(x => x.Project)
            .Include(x => x.Customer)
            .ToListAsync();
    }
}
