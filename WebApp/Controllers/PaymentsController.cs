using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpGet("invoice/{invoiceId}")]
    public async Task<IActionResult> GetByInvoiceId(int invoiceId)
    {
        return Ok(await _paymentService.GetPaymentsByInvoiceIdAsync(invoiceId));
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment(PaymentRegistrationForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _paymentService.AddPaymentAsync(form);
        return result ? Created("", null) : Problem();
    }

}
