using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class UserRoleFactory
{
    public static UserRolesEntity Create(int userId, int roleId) => new()
    {
        UserId = userId,
        RoleId = roleId
    };

    public static UserRole Map(UserRolesEntity entity) => new()
    {
        UserId = entity.UserId,
        FullName = $"{entity.User.FirstName} {entity.User.LastName}", 
        RoleId = entity.RoleId,
        RoleName = entity.Role.RoleName 
    };
}





