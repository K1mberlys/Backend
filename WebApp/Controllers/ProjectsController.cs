using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using Business.Services;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> Create(ProjectRegistrationForm form)
    {
        if (!ModelState.IsValid || form.CustomerId < 1)
            return BadRequest(ModelState);

        var result = await _projectService.CreateProjectAsync(form);
        return result ? Created("", null) : Problem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        
        return Ok(await _projectService.GetProjectsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        return project != null ? Ok(project) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProjectUpdateForm form)
    {
        if (!ModelState.IsValid || form.Id != id)
            return BadRequest(ModelState);

        var result = await _projectService.UpdateProjectAsync(form);
        return result ? NoContent() : NotFound();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projectService.DeleteProjectAsync(id);
        return result ? NoContent() : NotFound();
    }
}
