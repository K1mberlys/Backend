using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpPost]
    public async Task<IActionResult> Create(RoleForm form)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _roleService.CreateRoleAsync(form);
        return result ? Created("", null) : Problem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _roleService.GetAllRolesAsync());
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RoleForm form)
    {
        if (!ModelState.IsValid || form.Id != id)
            return BadRequest(ModelState);

        var result = await _roleService.UpdateRoleAsync(id, form);
        return result ? NoContent() : NotFound();
    }
}
