using Data.Enteties;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;
public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<CustomerAddressEntity> CustomerAddresses { get; set; } = [];
    public ICollection<ProjectEntity> Projects { get; set; } = [];
    public ICollection<InvoiceEntity> Invoices { get; set; } = [];
}
