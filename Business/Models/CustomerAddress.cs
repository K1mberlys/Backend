namespace Business.Models;

public class CustomerAddress
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public int AddressTypeId { get; set; }
    public string AddressType { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string AddressLine2 { get; set; } = null!;
    public string PostalCodeId { get; set; } = null!;
    
}
