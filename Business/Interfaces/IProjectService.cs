﻿using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationForm form);
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project?> GetProjectByIdAsync(int id);
        Task<bool> UpdateProjectAsync(ProjectUpdateForm form); 
        Task<bool> DeleteProjectAsync(int id);
    }
}
