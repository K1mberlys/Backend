using Business.Models;

namespace Business.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetInvoicesAsync();
        Task<bool> CreateInvoiceAsync(InvoiceRegistrationForm form); 
    }
}
