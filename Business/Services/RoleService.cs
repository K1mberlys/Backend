using Business.Interfaces;
using Data.Interfaces;
using Business.Factories;
using Business.Models;

namespace Business.Services;

public class RoleService(IRoleRepository roleRepository, IUserRepository userRepository) : IRoleService
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUserRepository _userRepository = userRepository; 

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        var entities = await _roleRepository.GetAllAsync();
        return entities.Select(RoleFactory.Map);
    }

    public async Task<bool> CreateRoleAsync(RoleForm form)
    {
        var entity = RoleFactory.Create(form);
        return await _roleRepository.AddAsync(entity);
    }

    public async Task<bool> UpdateRoleAsync(int id, RoleForm form)
    {
        if (id != form.Id)
            return false;

        var entity = RoleFactory.Create(form);
        return await _roleRepository.UpdateAsync(entity);
    }

    public async Task<bool> RemoveRoleAsync(int roleId)
    {
        var usersWithRole = await _userRepository.GetAllAsync();
        var usersToUpdate = usersWithRole.Where(u => u.RoleId == roleId).ToList();

        foreach (var user in usersToUpdate)
        {
            user.RoleId = null;
            await _userRepository.UpdateAsync(user);
        }

        var role = await _roleRepository.GetAsync(r => r.Id == roleId);
        if (role == null) return false;

        return await _roleRepository.RemoveAsync(role);
    }
}
