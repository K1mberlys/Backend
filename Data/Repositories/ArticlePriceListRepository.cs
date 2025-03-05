using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ArticlePriceListRepository(DataContext context) : BaseRepository<ArticlePriceListEntity>(context), IArticlePriceListRepository
{
    public async Task<IEnumerable<ArticlePriceListEntity>> GetPricesByArticleIdAsync(int articleId)
    {
        return await _context.ArticlePriceLists
            .Where(x => x.ArticleId == articleId)
            .ToListAsync();
    }
}