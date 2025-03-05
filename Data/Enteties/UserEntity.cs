using Data.Enteties;

public class UserEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string SecurityKey { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }

    public int? RoleId { get; set; } 
    public RoleEntity? Role { get; set; } 

    public ICollection<ProjectEntity> Projects { get; set; } = [];
    public ICollection<UserRolesEntity> UserRoles { get; set; } = [];
    public ICollection<UserAddressEntity> UserAddresses { get; set; } = [];
}
