namespace Business.Models;

public class UserAddress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public int AddressTypeId { get; set; }
    public string AddressType { get; set; } = null!;
    public string AddressLine_1 { get; set; } = null!;
    public string AddressLine_2 { get; set; } = null!;
    public string PostalCodeId { get; set; } = null!;
}
