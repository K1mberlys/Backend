using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserRegistrationForm form);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
