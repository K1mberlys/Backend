using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;
public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{

    public async Task<UserEntity?> GetUserCredentialsAsync(string email)
    {
        var entity = await _context.Users
            .Select(x => new UserEntity
            {
                Id = x.Id,
                Email = x.Email,
                Password = x.Password,
                SecurityKey = x.SecurityKey,
                EmailConfirmed = x.EmailConfirmed,
               
            })
            .FirstOrDefaultAsync(x => x.Email == email);

        return entity;
    }
    
}

