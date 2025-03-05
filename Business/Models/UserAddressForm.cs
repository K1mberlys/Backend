namespace Business.Models;

public class UserAddressForm
{
    public int UserId { get; set; }
    public int AddressTypeId { get; set; }
    public string AddressLine_1 { get; set; } = null!;
    public string AddressLine_2 { get; set; } = null!;
    public string PostalCodeId { get; set; } = null!;
}
