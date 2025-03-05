using Data.Enteties;
using Business.Models;

namespace Business.Factories;

public static class AddressTypeFactory
{
    public static AddressTypeEntity Create(string type) => new() { AddressType = type };
    public static AddressType Map(AddressTypeEntity entity) => new() 
    { 
        Id = entity.Id, 
        AddressTypeName = entity.AddressType 
    };
}
