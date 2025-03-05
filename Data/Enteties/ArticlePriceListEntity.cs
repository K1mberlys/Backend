using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Enteties;

public class ArticlePriceListEntity
{
    [Key]
    public int Id { get; set; } 

    [ForeignKey("Article")]
    public int ArticleId { get; set; }

    public ArticleEntity Article { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}

