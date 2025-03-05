using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests.Repositories_Tests;

public class CustomerAddressRepository_Tests
{
    private DataContext GetDataContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    private async Task SeedData(DataContext context)
    {
        await context.Customers.AddRangeAsync(TestData.CustomerEntities);
        await context.Addresses.AddRangeAsync(TestData.AddressTypeEntities);
        await context.CustomersAddresses.AddRangeAsync(TestData.CustomerAddressEntities);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddCustomerAddress()
    {
        // Arrange
        var context = GetDataContext();
        var repository = new CustomerAddressRepository(context);

        var newCustomerAddress = new CustomerAddressEntity
        {
            CustomerId = 1,
            AddressTypeId = 2,
            AddressLine_1 = "Testgata 1",
            AddressLine_2 = "Lägenhet 100",
            PostalCodeId = "12345"
        };

        // Act
        var addResult = await repository.AddAsync(newCustomerAddress);
        var savedAddress = await context.CustomersAddresses.FirstOrDefaultAsync(a => a.CustomerId == newCustomerAddress.CustomerId);

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedAddress);
        Assert.Equal(newCustomerAddress.AddressLine_1, savedAddress!.AddressLine_1);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCustomerAddresses()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new CustomerAddressRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(TestData.CustomerAddressEntities.Length, result.Count());
    }

    [Fact]
    public async Task GetByCustomerIdAsync_ShouldReturnAddressesForSpecificCustomer()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new CustomerAddressRepository(context);
        var customerId = TestData.CustomerAddressEntities[0].CustomerId;

        // Act
        var result = await repository.GetByCustomerIdAsync(customerId);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, ca => Assert.Equal(customerId, ca.CustomerId));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCustomerAddress()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new CustomerAddressRepository(context);

        var addressToUpdate = await context.CustomersAddresses.FirstOrDefaultAsync();
        Assert.NotNull(addressToUpdate);

        addressToUpdate.AddressLine_1 = "Uppdaterad adress 123";

        // Act - tagit hjälp av chatGPT 4o
        context.ChangeTracker.Clear(); 
        var updateResult = await repository.UpdateAsync(addressToUpdate);
        var updatedAddress = await context.CustomersAddresses.FindAsync(addressToUpdate.Id);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedAddress);
        Assert.Equal("Uppdaterad adress 123", updatedAddress!.AddressLine_1);
    }


    [Fact]
    public async Task RemoveAsync_ShouldRemoveCustomerAddress()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new CustomerAddressRepository(context);

        var addressToRemove = await context.CustomersAddresses.FirstOrDefaultAsync();
        Assert.NotNull(addressToRemove);

        // Act
        var removeResult = await repository.RemoveAsync(addressToRemove);
        var deletedAddress = await context.CustomersAddresses.FindAsync(addressToRemove.Id);

        // Assert
        Assert.True(removeResult);
        Assert.Null(deletedAddress);
    }
}
