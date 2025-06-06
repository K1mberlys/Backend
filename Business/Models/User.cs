﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Business.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public string Role { get; set; } = null!;
}
