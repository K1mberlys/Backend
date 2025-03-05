using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class ArticlePriceListFactory
{
    public static ArticlePriceListEntity Create(ArticlePriceListForm form) => new()
    {
        ArticleId = form.ArticleId,
        Price = form.Price
    };

    public static ArticlePriceList Map(ArticlePriceListEntity entity) => new()
    {
        ArticleId = entity.ArticleId,
        ArticleName = entity.Article.ArticleName, 
        Price = entity.Price
    };
}
