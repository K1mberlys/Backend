using Business.Models;

namespace Business.Interfaces;

public interface IProjectArticleService
{
    Task<IEnumerable<ProjectArticle>> GetAllAsync();
    Task<ProjectArticle?> GetByIdAsync(int id);
    Task<bool> CreateAsync(ProjectArticleForm form);
    Task<bool> UpdateAsync(int id, ProjectArticleForm form);
    Task<bool> DeleteAsync(int id);
}
