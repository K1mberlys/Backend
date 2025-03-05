using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectArticlesController(IProjectArticleService service) : ControllerBase
{
    private readonly IProjectArticleService _service = service;

    [HttpPost]
    public async Task<IActionResult> Create(ProjectArticleForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.CreateAsync(form);
        return result ? Created("", null) : Problem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var projectArticle = await _service.GetByIdAsync(id);
        return projectArticle != null ? Ok(projectArticle) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProjectArticleForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.UpdateAsync(id, form);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}
