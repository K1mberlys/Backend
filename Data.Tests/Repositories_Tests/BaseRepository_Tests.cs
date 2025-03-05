using Data.Contexts;
using Data.Enteties;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class TestRepository<TEntity>(DataContext context) : BaseRepository<TEntity>(context) where TEntity : class
{
}

namespace Data.Tests.Repositories_Tests
{
    public class BaseRepository_Tests
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

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var context = GetDataContext();
            await DataContextSeeder.SeedAsync(context);

            var repository = new TestRepository<StatusTypeEntity>(context);
            var newStatus = new StatusTypeEntity { StatusType = "Test Status" };

            // Act
            var result = await repository.AddAsync(newStatus);
            var savedStatus = await context.Statuses.FirstOrDefaultAsync(s => s.StatusType == newStatus.StatusType);

            // Assert
            Assert.True(result);
            Assert.NotNull(savedStatus);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var context = GetDataContext();
            await DataContextSeeder.SeedAsync(context);

            var repository = new TestRepository<StatusTypeEntity>(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(TestData.StatusTypeEntities.Length, result.Count());
        }

        [Fact]
        public async Task GetAsync_ShouldReturnCorrectEntity()
        {
            // Arrange
            var context = GetDataContext();
            await DataContextSeeder.SeedAsync(context);

            var repository = new TestRepository<StatusTypeEntity>(context);
            var testStatus = TestData.StatusTypeEntities[0];

            // Act
            var result = await repository.GetAsync(s => s.StatusType == testStatus.StatusType);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testStatus.StatusType, result!.StatusType);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenEntityExists()
        {
            // Arrange
            var context = GetDataContext();
            await DataContextSeeder.SeedAsync(context);

            var repository = new TestRepository<StatusTypeEntity>(context);
            var existingStatus = TestData.StatusTypeEntities[0];

            // Act
            var exists = await repository.ExistsAsync(s => s.StatusType == existingStatus.StatusType);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            // Arrange
            var context = GetDataContext();
            await DataContextSeeder.SeedAsync(context);
            var repository = new TestRepository<StatusTypeEntity>(context);

            var statusToUpdate = await context.Statuses.FirstOrDefaultAsync();
            Assert.NotNull(statusToUpdate);

            statusToUpdate!.StatusType = "Uppdaterad Status";

            // Act
            var updateResult = await repository.UpdateAsync(statusToUpdate);
            var updatedStatus = await context.Statuses.FindAsync(statusToUpdate.Id);

            // Assert
            Assert.True(updateResult);
            Assert.NotNull(updatedStatus);
            Assert.Equal("Uppdaterad Status", updatedStatus!.StatusType);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveEntity()
        {
            // Arrange
            var context = GetDataContext();
            await DataContextSeeder.SeedAsync(context);
            var repository = new TestRepository<StatusTypeEntity>(context);

            var statusToRemove = await context.Statuses.FirstOrDefaultAsync();
            Assert.NotNull(statusToRemove);

            // Act
            var removeResult = await repository.RemoveAsync(statusToRemove!);
            var deletedStatus = await context.Statuses.FindAsync(statusToRemove.Id);

            // Assert
            Assert.True(removeResult);
            Assert.Null(deletedStatus);
        }
    }
}
