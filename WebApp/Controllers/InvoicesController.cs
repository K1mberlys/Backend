using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController(IInvoiceService invoiceService) : ControllerBase
{
    private readonly IInvoiceService _invoiceService = invoiceService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _invoiceService.GetInvoicesAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(InvoiceRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _invoiceService.CreateInvoiceAsync(form);
        return result ? Created("", null) : Problem();
    }

}
