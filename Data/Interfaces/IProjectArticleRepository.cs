using Data.Enteties;

namespace Data.Interfaces;

public interface IProjectArticleRepository : IBaseRepository<ProjectArticleEntity>
{
    Task<IEnumerable<ProjectArticleEntity>> GetProjectIdAsync(int projectId);
}
