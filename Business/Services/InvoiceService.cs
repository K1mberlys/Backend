using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;

    public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
    {
        var entities = await _invoiceRepository.GetAllAsync();
        return entities.Select(InvoiceFactory.Map);
    }

    public async Task<bool> CreateInvoiceAsync(InvoiceRegistrationForm form)
    {
        var entity = InvoiceFactory.Create(form);
        return await _invoiceRepository.AddAsync(entity);
    }

}

