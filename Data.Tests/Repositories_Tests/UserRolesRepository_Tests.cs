using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Repositories_Tests
{
    public class UserRolesRepository_Tests
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
            if (!context.Roles.Any())
            {
                await context.Roles.AddRangeAsync(TestData.RoleEntities);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(TestData.UserEntities);
                await context.SaveChangesAsync();
            }

            if (!context.UserRoles.Any())
            {
                await context.UserRoles.AddRangeAsync(TestData.UserRolesEntities);
                await context.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task AddAsync_ShouldAddUserRole()
        {
            // Arrange
            var context = GetDataContext();
            var repository = new UserRolesRepository(context);

            var newUserRole = new UserRolesEntity { UserId = 3, RoleId = 2 };

            // Act
            var addResult = await repository.AddAsync(newUserRole);
            var savedUserRole = await context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == newUserRole.UserId && ur.RoleId == newUserRole.RoleId);

            // Assert
            Assert.True(addResult);
            Assert.NotNull(savedUserRole);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUserRoles()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new UserRolesRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(TestData.UserRolesEntities.Length, result.Count());
        }

        [Fact]
        public async Task GetRolesByUserIdAsync_ShouldReturnRolesForUser()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new UserRolesRepository(context);
            var userId = TestData.UserRolesEntities[0].UserId;

            // Act
            var result = await repository.GetRolesByUserIdAsync(userId);

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, r => Assert.Equal(userId, r.UserId));
        }

        [Fact]
        public async Task GetByIdsAsync_ShouldReturnCorrectUserRole()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new UserRolesRepository(context);
            var testUserRole = TestData.UserRolesEntities[0];

            // Act
            var result = await repository.GetByIdsAsync(testUserRole.UserId, testUserRole.RoleId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testUserRole.UserId, result!.UserId);
            Assert.Equal(testUserRole.RoleId, result.RoleId);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveUserRole() 
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new UserRolesRepository(context);

            var roleToRemove = await context.UserRoles.FirstOrDefaultAsync();
            Assert.NotNull(roleToRemove);

            // Act
            var removeResult = await repository.RemoveAsync(roleToRemove!);
            var removeRole = await context.UserRoles.FindAsync(roleToRemove!.UserId, roleToRemove.RoleId);

            // Assert
            Assert.True(removeResult);
            Assert.Null(removeRole);
        }
    }
}
