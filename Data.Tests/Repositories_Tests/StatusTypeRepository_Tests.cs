using Data.Contexts;
using Data.Enteties;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests.Repositories_Tests;

public class StatusTypeRepository_Tests
{
    //Tagit hjälp av ChatGPT 4o AddAsync_ShouldAddStatus()
    [Fact]
    public async Task AddAsync_ShouldAddStatus()
    {
        // Arrange
        var context = DataContextSeeder.GetDataContext();
        var statusRepository = new StatusTypeRepository(context);

        var newStatus = new StatusTypeEntity { StatusType = "Test Status" };

        // Act
        var addResult = await statusRepository.AddAsync(newStatus); 
        var result = await statusRepository.GetAsync(x => x.StatusType == newStatus.StatusType); 

        // Assert
        Assert.True(addResult); 
        Assert.NotNull(result); 
        Assert.NotEqual(0, result!.Id); 
    }


    [Fact]
    public async Task GetAllAsync_ShouldReturnAllStatuses()
    {
        //Arrange
        var context = DataContextSeeder.GetDataContext();

        await context.AddRangeAsync(TestData.StatusTypeEntities);
        await context.SaveChangesAsync();

        var statusRepository = new StatusTypeRepository(context);
        //Act
        var result = await statusRepository.GetAllAsync();


        //Assert
        Assert.Equal(TestData.StatusTypeEntities.Length, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnStatus()
    {
        //Arrange
        var context = DataContextSeeder.GetDataContext();

        await context.AddRangeAsync(TestData.StatusTypeEntities);
        await context.SaveChangesAsync();

        var statusRepository = new StatusTypeRepository(context);
        //Act
        var result = await statusRepository.GetAsync(x => x.StatusType == TestData.StatusTypeEntities[0].StatusType);


        //Assert
        Assert.Equal(TestData.StatusTypeEntities[0].StatusType, result!.StatusType);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue()
    {
        //Arrange
        var context = DataContextSeeder.GetDataContext();

        await context.AddRangeAsync(TestData.StatusTypeEntities);
        await context.SaveChangesAsync();

        var statusRepository = new StatusTypeRepository(context);
        var statusEntity = TestData.StatusTypeEntities[0];

        //Act
        statusEntity.StatusType = "Not Started";
        var result = await statusRepository.UpdateAsync(statusEntity);


        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnTrue()
    {
        //Arrange
        var context = DataContextSeeder.GetDataContext();

        await context.AddRangeAsync(TestData.StatusTypeEntities);
        await context.SaveChangesAsync();

        var statusRepository = new StatusTypeRepository(context);
        var statusEntity = TestData.StatusTypeEntities[0];

        //Act
        var result = await statusRepository.RemoveAsync(statusEntity);


        //Assert
        Assert.True(result);
    }
}