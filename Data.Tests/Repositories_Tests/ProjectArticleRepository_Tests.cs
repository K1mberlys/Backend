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
    public class ProjectArticleRepository_Tests
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
            if (!context.Projects.Any())
                await context.Projects.AddRangeAsync(TestData.ProjectEntities);

            if (!context.Articles.Any())
                await context.Articles.AddRangeAsync(TestData.ArticleEntities);

            if (!context.ProjectArticles.Any())
                await context.ProjectArticles.AddRangeAsync(TestData.ProjectArticleEntities);

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddProjectArticle()
        {
            // Arrange
            var context = GetDataContext();
            var repository = new ProjectArticleRepository(context);

            var newProjectArticle = new ProjectArticleEntity { ProjectId = 1, ArticleId = 2, Quantity = 5 };

            // Act
            var addResult = await repository.AddAsync(newProjectArticle);
            var savedProjectArticle = await context.ProjectArticles.FirstOrDefaultAsync(pa => pa.Id == newProjectArticle.Id);

            // Assert
            Assert.True(addResult);
            Assert.NotNull(savedProjectArticle);
            Assert.Equal(newProjectArticle.Quantity, savedProjectArticle!.Quantity);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProjectArticles()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new ProjectArticleRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(TestData.ProjectArticleEntities.Length, result.Count());
        }

        [Fact]
        public async Task GetProjectIdAsync_ShouldReturnArticlesForSpecificProject()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);

            var repository = new ProjectArticleRepository(context);
            var projectId = TestData.ProjectArticleEntities[0].ProjectId;

            // Act
            var result = await repository.GetProjectIdAsync(projectId);

            // Assert
            Assert.NotEmpty(result);
            Assert.All(result, pa => Assert.Equal(projectId, pa.ProjectId));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProjectArticleQuantity()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new ProjectArticleRepository(context);

            var projectArticleToUpdate = await context.ProjectArticles.FirstOrDefaultAsync();
            Assert.NotNull(projectArticleToUpdate);

            projectArticleToUpdate!.Quantity = 10;

            // Act
            var updateResult = await repository.UpdateAsync(projectArticleToUpdate);
            var updatedProjectArticle = await context.ProjectArticles.FindAsync(projectArticleToUpdate.Id);

            // Assert
            Assert.True(updateResult);
            Assert.NotNull(updatedProjectArticle);
            Assert.Equal(10, updatedProjectArticle!.Quantity);
        }

        [Fact]
        public async Task RemoveAsync_ShouldRemoveProjectArticle()
        {
            // Arrange
            var context = GetDataContext();
            await SeedData(context);
            var repository = new ProjectArticleRepository(context);

            var projectArticleToRemove = await context.ProjectArticles.FirstOrDefaultAsync();
            Assert.NotNull(projectArticleToRemove);

            // Act
            var removeResult = await repository.RemoveAsync(projectArticleToRemove);
            var deletedProjectArticle = await context.ProjectArticles.FindAsync(projectArticleToRemove.Id);

            // Assert
            Assert.True(removeResult);
            Assert.Null(deletedProjectArticle);
        }
    }
}
