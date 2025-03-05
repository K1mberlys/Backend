using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class ArticleEntity
{
    [Key]
    public int Id { get; set; }
    public string ArticleName { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsProduct { get; set; }

    public ICollection<ArticlePriceListEntity> ArticlePrice { get; set; } = new List<ArticlePriceListEntity>(); // ✅ Ändrat från 1-mot-1 till 1-mot-M
}

