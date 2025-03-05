using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class PaymentFactory
{
    public static PaymentEntity Create(PaymentRegistrationForm form) => new()
    {
        InvoiceId = form.InvoiceId,
        Amount = form.Amount,
        PaymentDate = form.PaymentDate
    };

    public static Payment Map(PaymentEntity entity) => new()
    {
        Id = entity.Id,
        InvoiceId = entity.InvoiceId,
        Amount = entity.Amount,
        PaymentDate = entity.PaymentDate
    };
}
