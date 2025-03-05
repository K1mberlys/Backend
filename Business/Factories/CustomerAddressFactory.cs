using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class CustomerAddressFactory
{
    public static CustomerAddressEntity Create(CustomerAddressForm form) => new()
    {
        CustomerId = form.CustomerId,
        AddressTypeId = form.AddressTypeId,
        AddressLine_1 = form.AddressLine1,
        AddressLine_2 = form.AddressLine2,
        PostalCodeId = form.PostalCode
    };

    public static CustomerAddress Map(CustomerAddressEntity entity) => new()
    {
        Id = entity.Id,
        CustomerId = entity.CustomerId,
        CustomerName = entity.Customer.CustomerName,
        AddressTypeId = entity.AddressTypeId,
        AddressType = entity.AddressType.AddressType,
        AddressLine1 = entity.AddressLine_1,
        AddressLine2 = entity.AddressLine_2,
        PostalCodeId = entity.PostalCodeId,
    };
}





