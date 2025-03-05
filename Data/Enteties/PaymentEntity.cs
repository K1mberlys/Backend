using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class PaymentEntity
{
    [Key]
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public InvoiceEntity Invoice { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}

