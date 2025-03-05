using Business.Interfaces;
using Data.Interfaces;
using Business.Factories;
using Business.Models;

namespace Business.Services;

public class ArticleService(IArticleRepository articleRepository) : IArticleService
{
    private readonly IArticleRepository _articleRepository = articleRepository;

    public async Task<IEnumerable<Article>> GetAllArticlesAsync()
    {
        var entities = await _articleRepository.GetAllAsync();
        return entities.Select(ArticleFactory.Map);
    }

    public async Task<Article?> GetArticleByIdAsync(int id)
    {
        var entity = await _articleRepository.GetAsync(a => a.Id == id);
        return entity != null ? ArticleFactory.Map(entity) : null;
    }

    public async Task<bool> CreateArticleAsync(ArticleRegistrationForm form)
    {
        var entity = ArticleFactory.Create(form);
        return await _articleRepository.AddAsync(entity);
    }

    public async Task<bool> UpdateArticleAsync(int id, ArticleUpdateForm form)
    {
        if (id != form.Id)
            return false;

        var entity = ArticleFactory.Create(form);
        return await _articleRepository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteArticleAsync(int id)
    {
        var entity = await _articleRepository.GetAsync(a => a.Id == id);
        return entity != null && await _articleRepository.RemoveAsync(entity);
    }
}
