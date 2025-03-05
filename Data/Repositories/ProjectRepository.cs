using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public async Task<IEnumerable<ProjectEntity>> GetProjectListAsync()
    {
        try
        {
            var entities = await _context.Projects
                .Include(x => x.StatusType)
                .Include(x => x.ProjectManager)
                .Include(x => x.Customer)
                .Include(x => x.ProjectArticles)
                    .ThenInclude(pa => pa.Article)
                .Select(x => new ProjectEntity
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    StatusType = new StatusTypeEntity
                    {
                        StatusType = x.StatusType.StatusType
                    },
                    ProjectManager = new UserEntity
                    {
                        FirstName = x.ProjectManager.FirstName,
                        LastName = x.ProjectManager.LastName,
                    },
                    ProjectArticles = x.ProjectArticles,
                    Customer = x.Customer,
                })
                .ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
       
    }

}




