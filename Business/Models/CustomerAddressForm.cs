namespace Business.Models;

public class CustomerAddressForm
{
    public int CustomerId { get; set; }
    public int AddressTypeId { get; set; }
    public string AddressLine1 { get; set; } = null!;
    public string AddressLine2 { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
