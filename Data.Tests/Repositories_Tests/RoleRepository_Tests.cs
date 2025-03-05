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
    public class RoleRepository_Tests
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
        }

        [Fact]
        public async Task AddAsync_ShouldAddRole()
        {
            // Arrange
            var context = GetDataContext();
            var repository = new RoleRepository(context);

            var newRole = new RoleEntity { RoleName = "Test Role" };

            // Act
            var addResult = await repository.AddAsync(newRole);
            var savedRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == newRole.RoleName);

            // Assert
            Assert.True(addResult);
            Assert.NotNull(savedRole);
            Assert.Equal("Test Role", savedRole!.RoleName);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRoles()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new RoleRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(TestData.RoleEntities.Length, result.Count());
        }

        [Fact]
        public async Task GetAsync_ShouldReturnRole()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new RoleRepository(context);
            var testRole = TestData.RoleEntities[0];

            // Act
            var result = await repository.GetAsync(x => x.Id == testRole.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testRole.RoleName, result!.RoleName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateRole()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new RoleRepository(context);

            var roleToUpdate = await context.Roles.FirstOrDefaultAsync();
            Assert.NotNull(roleToUpdate);

            roleToUpdate!.RoleName = "Updated Role";

            // Act
            var updateResult = await repository.UpdateAsync(roleToUpdate);
            var updatedRole = await context.Roles.FindAsync(roleToUpdate.Id);

            // Assert
            Assert.True(updateResult);
            Assert.NotNull(updatedRole);
            Assert.Equal("Updated Role", updatedRole!.RoleName);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveRole()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new RoleRepository(context);

            var roleToRemove = await context.Roles.FirstOrDefaultAsync();
            Assert.NotNull(roleToRemove);

            // Act
            var removeResult = await repository.RemoveAsync(roleToRemove);
            var deletedRole = await context.Roles.FindAsync(roleToRemove.Id);

            // Assert
            Assert.True(removeResult);
            Assert.Null(deletedRole);
        }

    }
}
