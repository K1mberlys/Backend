using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRolesController(IUserRoleService service) : ControllerBase
{
    private readonly IUserRoleService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllUserRolesAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(int userId, int roleId)
    {
        var result = await _service.AssignRoleToUserAsync(userId, roleId);
        return result ? Created("", null) : Conflict("User already has this role.");
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveRole(int userId, int roleId)
    {
        var result = await _service.RemoveUserRoleAsync(userId, roleId);
        return result ? NoContent() : NotFound("User or Role not found.");
    }
}
