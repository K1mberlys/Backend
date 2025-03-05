using Data.Enteties;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Enteties;

public class UserRolesEntity
{
   
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;



}

