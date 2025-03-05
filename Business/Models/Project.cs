using Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;
public class Project
{
    public int Id { get; set; }
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = null!; 
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!; 
    public int ProjectManagerId { get; set; }
    public string ProjectManagerName { get; set; } = null!; 
    public int ArticleId { get; set; }
    public string ArticleName { get; set; } = null!; 
}





