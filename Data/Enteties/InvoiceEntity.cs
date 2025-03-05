using Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class InvoiceEntity
{
    [Key]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    public decimal TotalAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }

    public ICollection<PaymentEntity> Payments { get; set; } = [];
}

