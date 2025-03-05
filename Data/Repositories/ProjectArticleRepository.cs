using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectArticleRepository(DataContext context) : BaseRepository<ProjectArticleEntity>(context), IProjectArticleRepository
{
    public async Task<IEnumerable<ProjectArticleEntity>> GetProjectIdAsync(int projectId)
    {
        return await _context.ProjectArticles
            .Include(x => x.Project)
            .Include(x => x.Article)
            .Where(x => x.ProjectId == projectId)
            .ToListAsync();
    }
}
