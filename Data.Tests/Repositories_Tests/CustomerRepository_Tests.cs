using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Data.Tests.Repositories_Tests
{
    public class CustomerRepository_Tests
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
            if (!context.Customers.Any())
            {
                await context.Customers.AddRangeAsync(TestData.CustomerEntities);
                await context.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task AddAsync_ShouldAddCustomer()
        {
            // Arrange
            var context = GetDataContext();
            var repository = new CustomerRepository(context);

            var newCustomer = new CustomerEntity { CustomerName = "New Customer", Email = "new@customer.com" };

            // Act
            var addResult = await repository.AddAsync(newCustomer);
            var savedCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Email == newCustomer.Email);

            // Assert
            Assert.True(addResult);
            Assert.NotNull(savedCustomer);
            Assert.Equal(newCustomer.CustomerName, savedCustomer!.CustomerName);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new CustomerRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(TestData.CustomerEntities.Length, result.Count());
        }

        [Fact]
        public async Task GetAsync_ShouldReturnCustomerWithProjects()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new CustomerRepository(context);
            var customerId = TestData.CustomerEntities[0].Id;

            // Act
            var result = await repository.GetAsync(c => c.Id == customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result!.Id);
            Assert.NotNull(result.Projects);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCustomer()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new CustomerRepository(context);

            var customerToUpdate = await context.Customers.FirstOrDefaultAsync();
            Assert.NotNull(customerToUpdate);

            customerToUpdate!.CustomerName = "Updated Customer";

            // Act
            var updateResult = await repository.UpdateAsync(customerToUpdate);
            var updatedCustomer = await context.Customers.FindAsync(customerToUpdate.Id);

            // Assert
            Assert.True(updateResult);
            Assert.NotNull(updatedCustomer);
            Assert.Equal("Updated Customer", updatedCustomer!.CustomerName);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveCustomer()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new CustomerRepository(context);

            var customerToRemove = await context.Customers.FirstOrDefaultAsync();
            Assert.NotNull(customerToRemove);

            // Act
            var removeResult = await repository.RemoveAsync(customerToRemove);
            var deletedCustomer = await context.Customers.FindAsync(customerToRemove.Id);

            // Assert
            Assert.True(removeResult);
            Assert.Null(deletedCustomer);
        }
    }
}
