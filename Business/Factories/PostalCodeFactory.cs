using Data.Enteties;
using Business.Models;

namespace Business.Factories;

public static class PostalCodeFactory
{
    public static PostalCodeEntity Create(PostalCodeForm form) => new()
    {
        PostalCode = form.PostalCodeId,
        City = form.City
    };

    public static PostalCode Map(PostalCodeEntity entity) => new()
    {
        PostalCodeId = entity.PostalCode,
        City = entity.City
    };
}
