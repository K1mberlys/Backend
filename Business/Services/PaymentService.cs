using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class PaymentService(IPaymentRepository paymentRepository) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;

    public async Task<IEnumerable<Payment>> GetPaymentsByInvoiceIdAsync(int invoiceId)
    {
        var entities = await _paymentRepository.GetPaymentsByInvoiceIdAsync(invoiceId);
        return entities.Select(PaymentFactory.Map);
    }

    public async Task<bool> AddPaymentAsync(PaymentRegistrationForm form)
    {
        var entity = PaymentFactory.Create(form);
        return await _paymentRepository.AddAsync(entity);
    }
}
