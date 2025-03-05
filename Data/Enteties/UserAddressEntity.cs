using Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class UserAddressEntity
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int AddressTypeId { get; set; }
    public AddressTypeEntity AddressType { get; set; } = null!;
    public string AddressLine_1 { get; set; } = null!;
    public string AddressLine_2 { get; set; } = null!;
    public string PostalCodeId { get; set; } = null!;
    public PostalCodeEntity PostalCode { get; set; } = null!;
}
