using Data.Enteties;
using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class StatusTypeEntity
{
    [Key]
    public int Id { get; set; }
    public string StatusType { get; set; } = null!;
    public ICollection<ProjectEntity> Projects { get; set; } = [];
}

