using Data.Contexts;
using Data.Enteties;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Repositories_Tests
{
    //Tagit hjälp av ChatGPT 4o
    public class InvoiceRepository_Tests
    {
        private DataContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .EnableSensitiveDataLogging()
                .Options;

            var context = new DataContext(options);
            context.Database.EnsureDeleted(); 
            context.Database.EnsureCreated(); 

            return context;
        }

        private async Task SeedData(DataContext context)
        {
            
            var project = new ProjectEntity
            {
                ProjectName = "Test Project",
                Description = "Test Description",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                StatusTypeId = 1,
                CustomerId = 1,
                ProjectManagerId = 1,
                
            };

            var customer = new CustomerEntity
            {
                CustomerName = "Test Customer",
                Email = "customer@test.com"
            };

            await context.Projects.AddAsync(project);
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync(); 

            var invoice = new InvoiceEntity
            {
                ProjectId = project.Id,  
                CustomerId = customer.Id, 
                TotalAmount = 5000.0m,
                IssueDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                IsPaid = false
            };

            await context.Invoices.AddAsync(invoice);
            await context.SaveChangesAsync(); 

            
            Assert.True(await context.Invoices.AnyAsync());
            Assert.True(await context.Projects.AnyAsync());
            Assert.True(await context.Customers.AnyAsync());
        }

        [Fact]
        public async Task AddAsync_ShouldAddInvoice()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new InvoiceRepository(context);

            var newInvoice = new InvoiceEntity
            {
                ProjectId = 1,
                CustomerId = 1,
                TotalAmount = 6000.0m,
                IssueDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(45),
                IsPaid = false
            };

            // Act
            var result = await repository.AddAsync(newInvoice);
            var savedInvoice = await context.Invoices.FirstOrDefaultAsync(i => i.TotalAmount == 6000.0m);

            // Assert
            Assert.True(result);
            Assert.NotNull(savedInvoice);
            Assert.Equal(6000.0m, savedInvoice!.TotalAmount);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllInvoices()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new InvoiceRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result); 
        }

        [Fact]
        public async Task GetInvoicesWithProjectsAsync_ShouldReturnInvoicesWithProjects()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new InvoiceRepository(context);

            // Act
            var result = await repository.GetInvoicesWithProjectsAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, invoice => Assert.NotNull(invoice.Project)); 
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateInvoice()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new InvoiceRepository(context);

            var invoiceToUpdate = await context.Invoices.FirstOrDefaultAsync();
            Assert.NotNull(invoiceToUpdate);

            invoiceToUpdate!.IsPaid = true;

            // Act
            var result = await repository.UpdateAsync(invoiceToUpdate);
            var updatedInvoice = await context.Invoices.FindAsync(invoiceToUpdate.Id);

            // Assert
            Assert.True(result);
            Assert.NotNull(updatedInvoice);
            Assert.True(updatedInvoice!.IsPaid);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveInvoice()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new InvoiceRepository(context);

            var invoiceToRemove = await context.Invoices.FirstOrDefaultAsync();
            Assert.NotNull(invoiceToRemove);

            // Act
            var result = await repository.RemoveAsync(invoiceToRemove);
            var deletedInvoice = await context.Invoices.FindAsync(invoiceToRemove.Id);

            // Assert
            Assert.True(result);
            Assert.Null(deletedInvoice);
        }
    }
}
