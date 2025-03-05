using Data.Enteties;

namespace Data.Interfaces;

public interface IArticlePriceListRepository : IBaseRepository<ArticlePriceListEntity>
{
    Task<IEnumerable<ArticlePriceListEntity>> GetPricesByArticleIdAsync(int articleId);
}