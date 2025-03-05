using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Enteties;

public class CustomerAddressEntity
{
    [Key]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
    public int AddressTypeId { get; set; }
    public AddressTypeEntity AddressType { get; set; } = null!;

    public string AddressLine_1 { get; set; } = null!;
    public string AddressLine_2 { get; set; } = null!;
    public string PostalCodeId { get; set; } = null!;
    public PostalCodeEntity PostalCode { get; set; } = null!;
}
