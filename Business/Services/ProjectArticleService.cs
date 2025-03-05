using Business.Interfaces;
using Data.Interfaces;
using Business.Factories;
using Business.Models;

namespace Business.Services;

public class ProjectArticleService(IProjectArticleRepository repository) : IProjectArticleService
{
    private readonly IProjectArticleRepository _repository = repository;

    public async Task<IEnumerable<ProjectArticle>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(ProjectArticleFactory.Map);
    }

    public async Task<ProjectArticle?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetAsync(pa => pa.Id == id);
        return entity != null ? ProjectArticleFactory.Map(entity) : null;
    }

    public async Task<bool> CreateAsync(ProjectArticleForm form)
    {
        var entity = ProjectArticleFactory.Create(form);
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(int id, ProjectArticleForm form)
    {
        var existing = await _repository.GetAsync(pa => pa.Id == id);
        if (existing == null) return false;

        existing.ProjectId = form.ProjectId;
        existing.ArticleId = form.ArticleId;
        existing.Quantity = form.Quantity;

        return await _repository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetAsync(pa => pa.Id == id);
        return entity != null && await _repository.RemoveAsync(entity);
    }
}
