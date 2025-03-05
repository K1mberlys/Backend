using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class PostalCodeEntity
{
    [Key]
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}