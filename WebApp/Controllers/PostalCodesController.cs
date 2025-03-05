using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostalCodesController(IPostalCodeService service) : ControllerBase
{
    private readonly IPostalCodeService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{postalCodeId}")]
    public async Task<IActionResult> GetById(string postalCodeId)
    {
        var postalCode = await _service.GetByIdAsync(postalCodeId);
        return postalCode != null ? Ok(postalCode) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PostalCodeForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.CreateAsync(form);
        return result ? Created("", null) : Problem();
    }

    [HttpPut("{postalCodeId}")]
    public async Task<IActionResult> Update(string postalCodeId, PostalCodeForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.UpdateAsync(postalCodeId, form);
        return result ? NoContent() : NotFound();
    }

    [HttpDelete("{postalCodeId}")]
    public async Task<IActionResult> Delete(string postalCodeId)
    {
        var result = await _service.DeleteAsync(postalCodeId);
        return result ? NoContent() : NotFound();
    }
}
