using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Repositories_Tests;

public class AddressTypeRepository_Tests
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
        await context.Addresses.AddRangeAsync(TestData.AddressTypeEntities);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddAddressType()
    {
        // Arrange
        var context = GetDataContext();
        var repository = new AddressTypeRepository(context);

        var newAddressType = new AddressTypeEntity { AddressType = "Leveransadress" };

        // Act
        var addResult = await repository.AddAsync(newAddressType);
        var savedAddressType = await context.Addresses.FirstOrDefaultAsync(a => a.AddressType == newAddressType.AddressType);

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedAddressType);
        Assert.NotEqual(0, savedAddressType!.Id);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAddressTypes()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        IAddressTypeRepository repository = new AddressTypeRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotEmpty(result); 
        Assert.Equal(TestData.AddressTypeEntities.Length, result.Count()); 
    }

    [Fact]
    public async Task GetAsync_ShouldReturnAddressType()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new AddressTypeRepository(context);
        var testAddressType = TestData.AddressTypeEntities[0];

        // Act
        var result = await repository.GetAsync(x => x.Id == testAddressType.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testAddressType.AddressType, result!.AddressType);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateAddressType()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new AddressTypeRepository(context);
        var existingAddressType = TestData.AddressTypeEntities[0];
        existingAddressType.AddressType = "Uppdaterad adresstyp";

        // Act
        var updateResult = await repository.UpdateAsync(existingAddressType);
        var updatedAddressType = await repository.GetAsync(x => x.Id == existingAddressType.Id);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedAddressType);
        Assert.Equal("Uppdaterad adresstyp", updatedAddressType!.AddressType);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveAddressType()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new AddressTypeRepository(context);
        var addressTypeToRemove = TestData.AddressTypeEntities[0];

        // Act
        var removeResult = await repository.RemoveAsync(addressTypeToRemove);
        var deletedAddressType = await repository.GetAsync(x => x.Id == addressTypeToRemove.Id);

        // Assert
        Assert.True(removeResult);
        Assert.Null(deletedAddressType);
    }
}
