using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlePriceListsController(IArticlePriceListService service) : ControllerBase
{
    private readonly IArticlePriceListService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllArticlePricesAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ArticlePriceListForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.CreateArticlePriceAsync(form);
        return result ? Created("", null) : Problem();
    }
}
