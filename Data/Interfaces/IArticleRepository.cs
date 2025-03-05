using Data.Enteties;

namespace Data.Interfaces;

public interface IArticleRepository : IBaseRepository<ArticleEntity>
{
    Task<IEnumerable<ArticleEntity>> GetAllWithPricesAsync();
}