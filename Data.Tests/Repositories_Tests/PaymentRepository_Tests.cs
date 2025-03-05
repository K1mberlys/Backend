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
    public class PaymentRepository_Tests
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
            if (!context.Invoices.Any())
                await context.Invoices.AddRangeAsync(TestData.InvoiceEntities);

            if (!context.Payments.Any())
                await context.Payments.AddRangeAsync(TestData.PaymentEntities);

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddPayment()
        {
            // Arrange
            var context = GetDataContext();
            var repository = new PaymentRepository(context);

            var newPayment = new PaymentEntity { InvoiceId = 1, Amount = 2000.0m, PaymentDate = DateTime.UtcNow };

            // Act
            var addResult = await repository.AddAsync(newPayment);
            var savedPayment = await context.Payments.FirstOrDefaultAsync(p => p.Id == newPayment.Id);

            // Assert
            Assert.True(addResult);
            Assert.NotNull(savedPayment);
            Assert.Equal(newPayment.Amount, savedPayment!.Amount);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPayments()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new PaymentRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(TestData.PaymentEntities.Length, result.Count());
        }

        [Fact]
        public async Task GetPaymentsByInvoiceIdAsync_ShouldReturnPaymentsForInvoice()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new PaymentRepository(context);
            var invoiceId = TestData.PaymentEntities[0].InvoiceId;

            // Act
            var result = await repository.GetPaymentsByInvoiceIdAsync(invoiceId);

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, p => Assert.Equal(invoiceId, p.InvoiceId));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePayment()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new PaymentRepository(context);

            var paymentToUpdate = await context.Payments.FirstOrDefaultAsync();
            Assert.NotNull(paymentToUpdate);

            paymentToUpdate!.Amount = 3000.0m;

            // Act
            var updateResult = await repository.UpdateAsync(paymentToUpdate);
            var updatedPayment = await context.Payments.FindAsync(paymentToUpdate.Id);

            // Assert
            Assert.True(updateResult);
            Assert.NotNull(updatedPayment);
            Assert.Equal(3000.0m, updatedPayment!.Amount);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemovePayment()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new PaymentRepository(context);

            var paymentToRemove = await context.Payments.FirstOrDefaultAsync();
            Assert.NotNull(paymentToRemove);

            // Act
            var removeResult = await repository.RemoveAsync(paymentToRemove);
            var deletedPayment = await context.Payments.FindAsync(paymentToRemove.Id);

            // Assert
            Assert.True(removeResult);
            Assert.Null(deletedPayment);
        }
    }
}
