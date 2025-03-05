using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class UserAddressFactory
{
    public static UserAddressEntity Create(UserAddressForm form) => new()
    {
        UserId = form.UserId,
        AddressTypeId = form.AddressTypeId,
        AddressLine_1 = form.AddressLine_1,
        AddressLine_2 = form.AddressLine_2,
        PostalCodeId = form.PostalCodeId
    };

    public static UserAddress Map(UserAddressEntity entity) => new()
    {
        Id = entity.Id,
        UserId = entity.UserId,
        FullName = $"{entity.User.FirstName} {entity.User.LastName}",
        AddressTypeId = entity.AddressTypeId,
        AddressType = entity.AddressType.AddressType,
        AddressLine_1 = entity.AddressLine_1,
        AddressLine_2 = entity.AddressLine_2,
        PostalCodeId = entity.PostalCodeId,
    };
}
