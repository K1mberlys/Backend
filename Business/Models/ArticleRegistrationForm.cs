namespace Business.Models;

public class ArticleRegistrationForm
{
    public string ArticleName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsProduct { get; set; }
}
