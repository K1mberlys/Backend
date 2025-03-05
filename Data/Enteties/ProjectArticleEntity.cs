using Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class ProjectArticleEntity
{
    [Key]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
    public int ArticleId { get; set; }
    public ArticleEntity Article { get; set; } = null!;
    public int Quantity { get; set; }
}

