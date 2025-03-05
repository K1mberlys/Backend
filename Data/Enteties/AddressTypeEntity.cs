using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class AddressTypeEntity
{
    [Key]
    public int Id {get; set; }
    public string AddressType { get; set; } = null!;
}
