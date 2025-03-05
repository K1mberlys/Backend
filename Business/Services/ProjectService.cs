using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form) 
    {
        if (!await _customerRepository.ExistsAsync(customer => customer.Id == form.CustomerId))
            return false;

        var projectEntity = ProjectFactory.Create(form);
        return await _projectRepository.AddAsync(projectEntity);
    }

    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {

        var entities = await _projectRepository.GetProjectListAsync();
        return entities.Select(ProjectFactory.Map);
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var entity = await _projectRepository.GetAsync(p => p.Id == id);
        return entity != null ? ProjectFactory.Map(entity) : null;
    }

    public async Task<bool> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var entity = ProjectFactory.Create(form);
        return await _projectRepository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var entity = await _projectRepository.GetAsync(p => p.Id == id);
        return entity != null && await _projectRepository.RemoveAsync(entity);
    } 
}
