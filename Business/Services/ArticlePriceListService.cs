using Business.Interfaces;
using Data.Interfaces;
using Business.Factories;
using Business.Models;

namespace Business.Services;

public class ArticlePriceListService(IArticlePriceListRepository repository) : IArticlePriceListService
{
    private readonly IArticlePriceListRepository _repository = repository;

    public async Task<IEnumerable<ArticlePriceList>> GetAllArticlePricesAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(ArticlePriceListFactory.Map);
    }

    public async Task<bool> CreateArticlePriceAsync(ArticlePriceListForm form)
    {
        var entity = ArticlePriceListFactory.Create(form);
        return await _repository.AddAsync(entity);
    }
}

