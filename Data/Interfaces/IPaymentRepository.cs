using Data.Enteties;

namespace Data.Interfaces;

public interface IPaymentRepository : IBaseRepository<PaymentEntity>
{
    Task<IEnumerable<PaymentEntity>> GetPaymentsByInvoiceIdAsync(int invoiceId);
}
