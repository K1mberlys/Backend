using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Business.Services;

public class UserService(IUserRepository userRepository, IRoleRepository roleRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<bool> CreateUserAsync(UserRegistrationForm form)  
    {
        if (!await _roleRepository.ExistsAsync(r => r.Id == form.RoleId))
            return false;  

        var userEntity = UserFactory.Create(form); 
        return await _userRepository.AddAsync(userEntity);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var entities = await _userRepository.GetAllAsync();
        return entities.Select(UserFactory.Map);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var entity = await _userRepository.GetAsync(u => u.Id == id);
        return entity != null ? UserFactory.Map(entity) : null;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var entity = await _userRepository.GetAsync(u => u.Id == id);
        return entity != null && await _userRepository.RemoveAsync(entity);
    }
}
