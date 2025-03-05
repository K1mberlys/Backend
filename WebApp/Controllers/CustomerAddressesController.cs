using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerAddressesController(ICustomerAddressService service) : ControllerBase
{
    private readonly ICustomerAddressService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var address = await _service.GetByIdAsync(id);
        return address != null ? Ok(address) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerAddressForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.CreateAsync(form);
        return result ? Created("", null) : Problem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CustomerAddressForm form)
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
