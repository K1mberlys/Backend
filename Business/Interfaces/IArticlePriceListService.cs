using Business.Models;

namespace Business.Interfaces;

public interface IArticlePriceListService
{
    Task<IEnumerable<ArticlePriceList>> GetAllArticlePricesAsync();
    Task<bool> CreateArticlePriceAsync(ArticlePriceListForm form);
}