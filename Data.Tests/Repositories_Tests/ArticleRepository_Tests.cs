using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Data.Tests.Repositories_Tests;

public class ArticleRepository_Tests
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
        context.Articles.AddRange(TestData.ArticleEntities);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddArticle()
    {
        // Arrange
        var context = GetDataContext();
        var articleRepository = new ArticleRepository(context);

        var newArticle = new ArticleEntity
        {
            ArticleName = "Test Article",
            Description = "This is a test article",
            IsProduct = true,
            ArticlePrice = new List<ArticlePriceListEntity>
            {
                 new ArticlePriceListEntity { Price = 100.0m }
            }

        };

        // Act
        var addResult = await articleRepository.AddAsync(newArticle);
        var savedArticle = await context.Articles.FirstOrDefaultAsync(x => x.ArticleName == newArticle.ArticleName);

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedArticle);
        Assert.Equal("Test Article", savedArticle!.ArticleName);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllArticles()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var articleRepository = new ArticleRepository(context);

        // Act
        var result = await articleRepository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.ArticleEntities.Length, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnArticle()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var articleRepository = new ArticleRepository(context);
        var testArticle = TestData.ArticleEntities[0];

        // Act
        var result = await articleRepository.GetAsync(x => x.Id == testArticle.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testArticle.ArticleName, result!.ArticleName);
    }

    [Fact]
    public async Task GetAllWithPricesAsync_ShouldReturnArticlesWithPrices()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var articleRepository = new ArticleRepository(context);

        // Act
        var result = await articleRepository.GetAllWithPricesAsync();

        // Assert
        Assert.All(result, article => Assert.NotNull(article.ArticlePrice));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateArticle()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var articleRepository = new ArticleRepository(context);
        var articleToUpdate = TestData.ArticleEntities[0];
        articleToUpdate.ArticleName = "Updated Article";

        // Act
        var updateResult = await articleRepository.UpdateAsync(articleToUpdate);
        var updatedArticle = await articleRepository.GetAsync(x => x.Id == articleToUpdate.Id);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedArticle);
        Assert.Equal("Updated Article", updatedArticle!.ArticleName);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveArticle()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var articleRepository = new ArticleRepository(context);
        var articleToRemove = TestData.ArticleEntities[0];

        // Act
        var removeResult = await articleRepository.RemoveAsync(articleToRemove);
        var deletedArticle = await articleRepository.GetAsync(x => x.Id == articleToRemove.Id);

        // Assert
        Assert.True(removeResult);
        Assert.Null(deletedArticle);
    }
}
