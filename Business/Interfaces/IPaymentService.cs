using Business.Models;

namespace Business.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPaymentsByInvoiceIdAsync(int invoiceId);
        Task<bool> AddPaymentAsync(PaymentRegistrationForm form); 
    }
}
