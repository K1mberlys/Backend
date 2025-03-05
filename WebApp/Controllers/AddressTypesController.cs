using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressTypesController(IAddressTypeService addressTypeService) : ControllerBase
{
    private readonly IAddressTypeService _addressTypeService = addressTypeService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _addressTypeService.GetAllAddressTypesAsync());
    }
}
