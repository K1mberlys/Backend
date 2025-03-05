using System.ComponentModel.DataAnnotations;
using Data.Enteties;

namespace Data.Enteties;

public class RoleEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;

    public ICollection<UserEntity> Users { get; set; } = [];
    public ICollection<UserRolesEntity> UserRoles { get; set; } = [];

}