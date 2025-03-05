namespace Business.Models;

public class UserRole
{
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}
