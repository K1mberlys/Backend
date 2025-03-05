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

public class UserAddressRepository_Tests 
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
        await context.Users.AddRangeAsync(TestData.UserEntities);
        await context.Addresses.AddRangeAsync(TestData.AddressTypeEntities);
        await context.UserAddresses.AddRangeAsync(TestData.UserAddressEntities);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddUserAddress()
    {
        // Arrange
        var context = GetDataContext();
        var repository = new UserAddressRepository(context);

        var newUserAddress = new UserAddressEntity
        {
            UserId = 1,
            AddressTypeId = 2,
            AddressLine_1 = "Testgata 1",
            AddressLine_2 = "Lägenhet 100",
            PostalCodeId = "12345"
        };

        // Act
        var addResult = await repository.AddAsync(newUserAddress);
        var savedAddress = await context.UserAddresses.FirstOrDefaultAsync(a => a.UserId == newUserAddress.UserId);

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedAddress);
        Assert.Equal(newUserAddress.AddressLine_1, savedAddress!.AddressLine_1);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUserAddresses()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new UserAddressRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(TestData.UserAddressEntities.Length, result.Count());
    }

    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnAddressesForSpecificUser()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new UserAddressRepository(context);
        var userId = TestData.UserAddressEntities[0].UserId;

        // Act
        var result = await repository.GetByUserIdAsync(userId);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, ca => Assert.Equal(userId, ca.UserId));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUserAddress()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new UserAddressRepository(context);

        var addressToUpdate = await context.UserAddresses.FirstOrDefaultAsync();
        Assert.NotNull(addressToUpdate);

        addressToUpdate.AddressLine_1 = "Uppdaterad adress 123";

        // Act - tagit hjälp av chatGPT 4o
        context.ChangeTracker.Clear();
        var updateResult = await repository.UpdateAsync(addressToUpdate);
        var updatedAddress = await context.UserAddresses.FindAsync(addressToUpdate.Id);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedAddress);
        Assert.Equal("Uppdaterad adress 123", updatedAddress!.AddressLine_1);
    }


    [Fact]
    public async Task RemoveAsync_ShouldRemoveUserAddress()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new UserAddressRepository(context);

        var addressToRemove = await context.UserAddresses.FirstOrDefaultAsync();
        Assert.NotNull(addressToRemove);

        // Act
        var removeResult = await repository.RemoveAsync(addressToRemove);
        var deletedAddress = await context.UserAddresses.FindAsync(addressToRemove.Id);

        // Assert
        Assert.True(removeResult);
        Assert.Null(deletedAddress);
    }
}
