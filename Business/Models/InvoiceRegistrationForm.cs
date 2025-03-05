namespace Business.Models;

public class InvoiceRegistrationForm
{
    public int ProjectId { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
}

