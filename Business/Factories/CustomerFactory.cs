using Business.Models;
using Data.Enteties;

namespace Business.Factories;
public static class CustomerFactory
{
    public static CustomerEntity Create(CustomerRegistrationForm form) => new()
    {
        CustomerName = form.CustomerName,
        Email = form.Email
    };

    public static Customer Map(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
        Email = entity.Email,
        Projects = entity.Projects?.Select(ProjectFactory.Map).ToList() ?? []
    };
}





