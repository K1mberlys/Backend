namespace Business.Models;

public class ArticleUpdateForm
{
    public int Id { get; set; } 
    public string ArticleName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsProduct { get; set; }
}
