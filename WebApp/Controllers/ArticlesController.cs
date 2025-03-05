using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController(IArticleService articleService) : ControllerBase
{
    private readonly IArticleService _articleService = articleService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _articleService.GetAllArticlesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        return article != null ? Ok(article) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ArticleRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _articleService.CreateArticleAsync(form);
        return result ? Created("", null) : Problem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ArticleUpdateForm form)
    {
        if (!ModelState.IsValid || form.Id != id)
            return BadRequest(ModelState);

        var result = await _articleService.UpdateArticleAsync(id, form);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _articleService.DeleteArticleAsync(id);
        return result ? NoContent() : NotFound();
    }
}
