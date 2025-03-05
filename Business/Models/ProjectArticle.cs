namespace Business.Models;

public class ProjectArticle
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public int ArticleId { get; set; }
    public string ArticleName { get; set; } = null!;
    public int Quantity { get; set; }
}
