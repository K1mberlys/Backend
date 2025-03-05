using Data.Enteties;

namespace Data.Tests.SeedData;  

public static class TestData
{
    public static readonly CustomerEntity[] CustomerEntities =
    [
       new CustomerEntity {Id = 1, CustomerName = "EPN Sverige AB", Email = "epn@domain.com"},
       new CustomerEntity {Id = 2, CustomerName = "Nackademin AB", Email = "nac@domain.com"},
       new CustomerEntity {Id = 3, CustomerName = "DesignBySi", Email = "designbysi@domain.com"}

    ];

    public static readonly RoleEntity[] RoleEntities =
    [
       new RoleEntity { Id = 1, RoleName = "Admin" },
       new RoleEntity { Id = 2, RoleName = "User" },
       new RoleEntity { Id = 3, RoleName = "Manager" }
    ];

    public static readonly UserEntity[] UserEntities =
    [
       new UserEntity {Id = 1, FirstName = "Kimberly", LastName = "Sseremba Hadjal", Email = "kim@domain.com", Password = "BytMig123", SecurityKey = "SecurityKey123", RoleId = 1, Role = RoleEntities[0]},
       new UserEntity {Id = 2, FirstName = "Hans", LastName = "Mattin-Lassei", Email = "hans@domain.com", Password = "BytMig123", SecurityKey = "SecurityKey123", RoleId = 2, Role = RoleEntities[1]},
       new UserEntity {Id = 3, FirstName = "Anna", LastName = "Svensson", Email = "anna@domain.com", Password = "BytMig123", SecurityKey = "SecurityKey123", RoleId = 3, Role = RoleEntities[2]}


    ];

    public static readonly UserRolesEntity[] UserRolesEntities =
    [
        new UserRolesEntity { UserId = 1, RoleId = 1 },
        new UserRolesEntity { UserId = 2, RoleId = 2 },
        new UserRolesEntity { UserId = 3, RoleId = 3 }
    ];

    public static readonly ProjectEntity[] ProjectEntities =
    [

       new ProjectEntity {
           ProjectName = "DatabasTeknik",
           Description = "Kurs i Databaser",
           StartDate = new DateTime(2025, 02, 03),
           EndDate = new DateTime(2025, 02, 28),
           StatusTypeId = 1,
           CustomerId = 2,
           ProjectManagerId = 1,
           },

        new ProjectEntity {
        Id = 2, 
        ProjectName = "Programmering C#",
        Description = "Kurs i programmering",
        StartDate = new DateTime(2025, 03, 01),
        EndDate = new DateTime(2025, 06, 30),
        StatusTypeId = 2,
        CustomerId = 3,
        ProjectManagerId = 2,
        }
        
    ];

    public static readonly InvoiceEntity[] InvoiceEntities =
    [
        new InvoiceEntity{Id = 1, ProjectId = TestData.ProjectEntities[0].Id,CustomerId = TestData.CustomerEntities[0].Id, TotalAmount = 5000.0m, IssueDate = DateTime.UtcNow, DueDate = DateTime.UtcNow.AddDays(30), IsPaid = false}
    ];

    public static readonly PaymentEntity[] PaymentEntities =
    [
        new PaymentEntity { Id = 1, InvoiceId = 1, Amount = 1500.0m, PaymentDate = DateTime.UtcNow },
        new PaymentEntity { Id = 2, InvoiceId = 1, Amount = 500.0m, PaymentDate = DateTime.UtcNow },
        new PaymentEntity { Id = 3, InvoiceId = 2, Amount = 3000.0m, PaymentDate = DateTime.UtcNow }
    ];

    public static readonly StatusTypeEntity[] StatusTypeEntities =
    [
        new StatusTypeEntity {StatusType = "Ej påbörjad"},
        new StatusTypeEntity {StatusType = "Pågår"},
        new StatusTypeEntity {StatusType = "Avslutad"}

    ];

    public static readonly ArticleEntity[] ArticleEntities =
    [
        new ArticleEntity
        {
            Id = 1,
            ArticleName = "Consulting Service",
            Description = "IT Consulting, per hour",
            IsProduct = false,
        },
        new ArticleEntity
        {
             Id = 2,
            ArticleName = "Software License",
            Description = "Annual subscription",
            IsProduct = true,
        },
        new ArticleEntity
        {
             Id = 3,
             ArticleName = "Development Package",
             Description = "Custom software development package",
             IsProduct = true,
        }
    ];

    public static readonly ProjectArticleEntity[] ProjectArticleEntities =
    [
       new ProjectArticleEntity { Id = 1, ProjectId = 1, ArticleId = 1, Quantity = 3 },
       new ProjectArticleEntity { Id = 2, ProjectId = 1, ArticleId = 2, Quantity = 2 },
       new ProjectArticleEntity { Id = 3, ProjectId = 2, ArticleId = 3, Quantity = 5 }
    ];


    public static readonly ArticlePriceListEntity[] ArticlePriceListEntities =
    [
        new ArticlePriceListEntity { Id = 1, ArticleId = 1, Price = 1000.0m },
        new ArticlePriceListEntity { Id = 2, ArticleId = 2, Price = 5000.0m },
        new ArticlePriceListEntity { Id = 3, ArticleId = 3, Price = 20000.0m }
    ];


    public static readonly AddressTypeEntity[] AddressTypeEntities =
    [
        new AddressTypeEntity { Id = 1, AddressType = "Faktureringsadress" },
        new AddressTypeEntity { Id = 2, AddressType = "Leveransadress" },
        new AddressTypeEntity { Id = 3, AddressType = "Folkbokföringsadress" }
    ];

    public static readonly PostalCodeEntity[] PostalCodeEntities =
    [
        new PostalCodeEntity { PostalCode = "12345", City = "Stockholm" },
        new PostalCodeEntity { PostalCode = "67891", City = "Västerås" },
        new PostalCodeEntity { PostalCode = "54321", City = "Malmö" },   
        new PostalCodeEntity { PostalCode = "67890", City = "Göteborg" } 
    ];


    public static readonly CustomerAddressEntity[] CustomerAddressEntities =
    [
        new CustomerAddressEntity { Id = 1, CustomerId = 1, AddressTypeId = 1, AddressLine_1 = "Testgata 1", AddressLine_2 = "Lgh 1", PostalCodeId = "12345" },
        new CustomerAddressEntity { Id = 2, CustomerId = 2, AddressTypeId = 2, AddressLine_1 = "Testgata 2", AddressLine_2 = "Lgh 2", PostalCodeId = "54321" },
        new CustomerAddressEntity { Id = 3, CustomerId = 3, AddressTypeId = 3, AddressLine_1 = "Testgata 3", AddressLine_2 = "Lgh 3", PostalCodeId = "67890" }
    ];

    public static readonly UserAddressEntity[] UserAddressEntities =
    [
        new UserAddressEntity { Id = 1, UserId = 1, AddressTypeId = 1, AddressLine_1 = "Testgata 1", AddressLine_2 = "Lgh 1", PostalCodeId = "12345" },
        new UserAddressEntity { Id = 2, UserId = 2, AddressTypeId = 2, AddressLine_1 = "Testgata 2", AddressLine_2 = "Lgh 2", PostalCodeId = "54321" },
        new UserAddressEntity { Id = 3, UserId = 3, AddressTypeId = 3, AddressLine_1 = "Testgata 3", AddressLine_2 = "Lgh 3", PostalCodeId = "67890" }
    ];



}
