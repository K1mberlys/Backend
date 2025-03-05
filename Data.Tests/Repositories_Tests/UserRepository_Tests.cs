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

public class UserRepository_Tests
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
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddUser()
    {
        // Arrange
        var context = GetDataContext();
        var userRepository = new UserRepository(context);

        var newUser = new UserEntity
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@domain.com",
            Password = "Password123",
            SecurityKey = "SecurityKey123"
        };

        // Act
        var addResult = await userRepository.AddAsync(newUser);
        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedUser);
        Assert.NotEqual(0, savedUser!.Id);
    }

    [Fact]
    public async Task GetUsersAsync_ShouldReturnAllUsers()
    {
        //Arrange
        var context = GetDataContext();
        context.Users.AddRange(TestData.UserEntities);
        await context.SaveChangesAsync();

        IUserRepository userRepository = new UserRepository(context);

        // Act
        var result = await userRepository.GetAllAsync();


        // Assert
        Assert.Equal(result.Count(), TestData.UserEntities.Length);
    }


    [Fact]
    public async Task GetAsync_ShouldReturnUser()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context); 


        var userRepository = new UserRepository(context);
        var testUser = TestData.UserEntities[0];

        // Act
        var result = await userRepository.GetAsync(x => x.Id == testUser.Id); 

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testUser.Id, result!.Id);
    }

    [Fact]
    public async Task GetUserCredentialsAsync_ShouldReturnUserCredentials()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var userRepository = new UserRepository(context);

        var testUser = TestData.UserEntities[0];

        // Act
        var result = await userRepository.GetUserCredentialsAsync(testUser.Email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testUser.Email, result.Email);
        Assert.Equal(testUser.Password, result.Password);
        Assert.Equal(testUser.SecurityKey, result.SecurityKey);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUser()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var userRepository = new UserRepository(context);

        var existingUser = await context.Users.FirstOrDefaultAsync(); 
        Assert.NotNull(existingUser);

        existingUser!.FirstName = "UpdatedName";

        // Act
        var updateResult = await userRepository.UpdateAsync(existingUser);
        var updatedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == existingUser.Id);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedUser);
        Assert.Equal("UpdatedName", updatedUser!.FirstName);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveUser()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var userRepository = new UserRepository(context);

        var userToDelete = await context.Users.FirstOrDefaultAsync(); 
        Assert.NotNull(userToDelete);

        // Act
        var removeResult = await userRepository.RemoveAsync(userToDelete!);
        var deletedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userToDelete!.Id);

        // Assert
        Assert.True(removeResult);
    }

  

}
