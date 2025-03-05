using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusTypesController(IStatusTypeService statusTypeService) : ControllerBase
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _statusTypeService.GetStatusTypesAsync());
    }
}


