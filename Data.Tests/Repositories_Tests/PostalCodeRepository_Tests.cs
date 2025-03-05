using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Repositories_Tests;

public class PostalCodeRepository_Tests
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
        if (!context.Postalcodes.Any())
        {
            await context.Postalcodes.AddRangeAsync(TestData.PostalCodeEntities);
            await context.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task AddAsync_ShouldAddPostalCode()
    {
        // Arrange
        var context = GetDataContext();
        var repository = new PostalCodeRepository(context);

        var newPostalCode = new PostalCodeEntity { PostalCode = "54321", City = "Malmö" };

        // Act
        var addResult = await repository.AddAsync(newPostalCode);
        var savedPostalCode = await context.Postalcodes.FirstOrDefaultAsync(p => p.PostalCode == newPostalCode.PostalCode);

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedPostalCode);
        Assert.Equal("Malmö", savedPostalCode!.City);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPostalCodes()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new PostalCodeRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.PostalCodeEntities.Length, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnPostalCode()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new PostalCodeRepository(context);
        var testPostalCode = TestData.PostalCodeEntities[0].PostalCode;

        // Act
        var result = await repository.GetAsync(x => x.PostalCode == testPostalCode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testPostalCode, result!.PostalCode);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdatePostalCode()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new PostalCodeRepository(context);

        var postalCodeToUpdate = await context.Postalcodes.FirstOrDefaultAsync();
        Assert.NotNull(postalCodeToUpdate);

        postalCodeToUpdate!.City = "Ny stad";

        // Act
        var updateResult = await repository.UpdateAsync(postalCodeToUpdate);
        var updatedPostalCode = await context.Postalcodes.FindAsync(postalCodeToUpdate.PostalCode);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedPostalCode);
        Assert.Equal("Ny stad", updatedPostalCode!.City);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemovePostalCode()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new PostalCodeRepository(context);

        var postalCodeToRemove = await context.Postalcodes.FirstOrDefaultAsync();
        Assert.NotNull(postalCodeToRemove);

        // Act
        var removeResult = await repository.RemoveAsync(postalCodeToRemove);
        var deletedPostalCode = await context.Postalcodes.FindAsync(postalCodeToRemove.PostalCode);

        // Assert
        Assert.True(removeResult);
        Assert.Null(deletedPostalCode);
    }
}
