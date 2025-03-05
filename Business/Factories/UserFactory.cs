using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class UserFactory
{
    public static UserEntity Create(UserRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Email = form.Email,
        Password = form.Password, 
        RoleId = form.RoleId
    };
    public static User Map(UserEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Email = entity.Email
    };

}
