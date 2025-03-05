namespace Business.Models;

public class ArticlePriceList
{
    public int ArticleId { get; set; }
    public string ArticleName { get; set; } = null!;
    public decimal Price { get; set; }
}
