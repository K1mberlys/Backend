namespace Business.Models;

public class Invoice
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!; 
    public decimal TotalAmount { get; set; } 
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }

}
