using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Data.Repositories;

public class PaymentRepository(DataContext context) : BaseRepository<PaymentEntity>(context), IPaymentRepository
{
    public async Task<IEnumerable<PaymentEntity>> GetPaymentsByInvoiceIdAsync(int invoiceId)
    {
        return await _context.Payments
            .Include(x => x.Invoice)
            .Where(x  => x.InvoiceId == invoiceId)
            .ToListAsync();
    }
}
