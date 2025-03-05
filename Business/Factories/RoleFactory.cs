using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class RoleFactory
{
    public static RoleEntity Create(RoleForm form) => new()
    {
        RoleName = form.RoleName
    };

    public static Role Map(RoleEntity entity) => new()
    {
        Id = entity.Id,
        RoleName = entity.RoleName
    };
}
