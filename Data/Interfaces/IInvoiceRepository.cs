using Data.Enteties;

namespace Data.Interfaces;

public interface IInvoiceRepository : IBaseRepository<InvoiceEntity>
{
    Task<IEnumerable<InvoiceEntity>> GetInvoicesWithProjectsAsync();
}
