namespace Business.Models;

public class PaymentRegistrationForm
{
    public int InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
