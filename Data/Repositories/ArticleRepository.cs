using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ArticleRepository(DataContext context) : BaseRepository<ArticleEntity>(context), IArticleRepository
{
    public async Task<IEnumerable<ArticleEntity>> GetAllWithPricesAsync()
    {
        return await _context.Articles
            .Include(x => x.ArticlePrice) 
            .ToListAsync();
    }
}
