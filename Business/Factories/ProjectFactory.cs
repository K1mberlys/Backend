using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        ProjectName = form.ProjectName,
        Description = form.Description,
        CustomerId = form.CustomerId,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        StatusTypeId = form.StatusTypeId,
        ProjectManagerId = form.ProjectManagerId
    };

    public static ProjectEntity Create(ProjectUpdateForm form) => new()
    {
        Id = form.Id,
        ProjectName = form.ProjectName,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        StatusTypeId = form.StatusTypeId,
        CustomerId = form.CustomerId,
        ProjectManagerId = form.ProjectManagerId
    };

    public static Project Map(ProjectEntity entity) => new ()
    {
        Id = entity.Id,
        ProjectName = entity.ProjectName,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        Status = entity.StatusType.StatusType,
        CustomerId = entity.CustomerId, 
        CustomerName = entity.Customer.CustomerName,
        ProjectManagerId = entity.ProjectManagerId, 
        ProjectManagerName = $"{entity.ProjectManager.FirstName} {entity.ProjectManager.LastName}"
    };
}
