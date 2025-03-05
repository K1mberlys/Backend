using Business.Models;

namespace Business.Interfaces;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllArticlesAsync();
    Task<Article?> GetArticleByIdAsync(int id);
    Task<bool> CreateArticleAsync(ArticleRegistrationForm form);
    Task<bool> UpdateArticleAsync(int id, ArticleUpdateForm form);
    Task<bool> DeleteArticleAsync(int id);
}
