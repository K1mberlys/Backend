﻿namespace Business.Models;

public class ProjectUpdateForm
{
    public int Id { get; set; } 
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusTypeId { get; set; }
    public int CustomerId { get; set; }
    public int ProjectManagerId { get; set; }
    
}
