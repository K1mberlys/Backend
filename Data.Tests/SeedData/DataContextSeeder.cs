using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.SeedData;

public static class DataContextSeeder
{
    public static DataContext GetDataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();

        return context;

    }

    public static async Task SeedAsync(DataContext context)
    {


        context.Roles.AddRange(TestData.RoleEntities);

        context.Customers.AddRange(TestData.CustomerEntities);

        context.CustomersAddresses.AddRange(TestData.CustomerAddressEntities);

        context.Users.AddRange(TestData.UserEntities);

        context.UserRoles.AddRange(TestData.UserRolesEntities);

        context.Invoices.AddRange(TestData.InvoiceEntities);

        context.Payments.AddRange(TestData.PaymentEntities);

        context.Statuses.AddRange(TestData.StatusTypeEntities);

        context.Articles.AddRange(TestData.ArticleEntities);

        context.ProjectArticles.AddRange(TestData.ProjectArticleEntities);

        context.ArticlePriceLists.AddRange(TestData.ArticlePriceListEntities);

        context.Addresses.AddRange(TestData.AddressTypeEntities);

        context.CustomersAddresses.AddRange(TestData.CustomerAddressEntities);

        context.UserAddresses.AddRange(TestData.UserAddressEntities);

        context.Postalcodes.AddRange(TestData.PostalCodeEntities);

        await context.SaveChangesAsync();

    }
    //Tagit Hjälp av ChatGPT 4o
    //public static async Task SeedWithProjectAsync(DataContext context)
    //{

    //    context.Roles.AddRange(TestData.RoleEntities);

    //    context.Customers.AddRange(TestData.CustomerEntities);

    //    context.CustomersAddresses.AddRange(TestData.CustomerAddressEntities);

    //    context.Users.AddRange(TestData.UserEntities);

    //    context.UserRoles.AddRange(TestData.UserRolesEntities);

    //    context.Invoices.AddRange(TestData.InvoiceEntities);

    //    context.Payments.AddRange(TestData.PaymentEntities);

    //    context.Statuses.AddRange(TestData.StatusTypeEntities);

    //    context.Articles.AddRange(TestData.ArticleEntities);

    //    context.ProjectArticles.AddRange(TestData.ProjectArticleEntities);

    //    context.ArticlePriceLists.AddRange(TestData.ArticlePriceListEntities);

    //    context.Addresses.AddRange(TestData.AddressTypeEntities);

    //    context.CustomersAddresses.AddRange(TestData.CustomerAddressEntities);

    //    context.UserAddresses.AddRange(TestData.UserAddressEntities);

    //    context.Postalcodes.AddRange(TestData.PostalCodeEntities);

    //    context.Projects.AddRange(TestData.ProjectEntities);


    //    await context.SaveChangesAsync();




    //}
}

