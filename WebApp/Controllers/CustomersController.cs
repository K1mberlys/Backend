using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _customerService.GetCustomersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return customer != null ? Ok(customer) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _customerService.CreateCustomerAsync(form);
        return result ? Created("", null) : Problem();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        return result ? NoContent() : NotFound();
    }
}
