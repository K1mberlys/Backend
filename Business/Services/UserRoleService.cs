using Business.Factories;
using Business.Interfaces;
using Data.Interfaces;
using Data.Repositories;
using Business.Models;

namespace Business.Services;

public class UserRoleService(IUserRolesRepository repository) : IUserRoleService
{
    private readonly IUserRolesRepository _repository = repository;

    public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(UserRoleFactory.Map);
    }

    public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
    {
        var entity = UserRoleFactory.Create(userId, roleId);
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> RemoveUserRoleAsync(int userId, int roleId)
    {
        var entity = await _repository.GetByIdsAsync(userId, roleId);
        if (entity == null) return false;

        return await _repository.RemoveAsync(entity); 
    }
}

