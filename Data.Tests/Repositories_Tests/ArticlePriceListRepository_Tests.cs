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

public class ArticlePriceListRepository_Tests
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
    //Tagit hjälp av chatGPT 4o
    private async Task SeedData(DataContext context)
    {
        if (!context.Articles.Any())
        {
            await context.Articles.AddRangeAsync(TestData.ArticleEntities);
            await context.SaveChangesAsync();
        }

        foreach (var price in TestData.ArticlePriceListEntities)
        {
            var existingPrice = await context.ArticlePriceLists
                .FirstOrDefaultAsync(p => p.Id == price.Id);

            if (existingPrice == null)
            {
                await context.ArticlePriceLists.AddAsync(price);
            }
        }

        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddArticlePrice()
    {
        // Arrange
        var context = GetDataContext();
        var repository = new ArticlePriceListRepository(context);

        var newArticlePrice = new ArticlePriceListEntity { Id = 4, Price = 1500.0m };

        // Act
        var addResult = await repository.AddAsync(newArticlePrice);
        var savedPrice = await context.ArticlePriceLists.FirstOrDefaultAsync(a => a.Id == newArticlePrice.Id);

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedPrice);
        Assert.Equal(newArticlePrice.Price, savedPrice!.Price);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPrices()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new ArticlePriceListRepository(context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(TestData.ArticlePriceListEntities.Length, result.Count());
    }

    [Fact]
    public async Task GetPricesByArticleIdAsync_ShouldReturnPricesForSpecificArticle()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);

        var repository = new ArticlePriceListRepository(context);

        var articleId = (await context.Articles.FirstOrDefaultAsync())!.Id;

        // Act
        var result = await repository.GetPricesByArticleIdAsync(articleId);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, p => Assert.Equal(articleId, p.ArticleId));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateArticlePrice()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new ArticlePriceListRepository(context);

        var priceToUpdate = await context.ArticlePriceLists.FirstOrDefaultAsync();
        Assert.NotNull(priceToUpdate);

        priceToUpdate!.Price = 1200.0m;

        // Act
        var updateResult = await repository.UpdateAsync(priceToUpdate);
        var updatedPrice = await context.ArticlePriceLists.FindAsync(priceToUpdate.Id);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedPrice);
        Assert.Equal(1200.0m, updatedPrice!.Price);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveArticlePrice()
    {
        // Arrange
        var context = GetDataContext();
        await SeedData(context);
        var repository = new ArticlePriceListRepository(context);

        var priceToRemove = await context.ArticlePriceLists.FirstOrDefaultAsync(); 
        Assert.NotNull(priceToRemove);

        // Act
        var removeResult = await repository.RemoveAsync(priceToRemove);
        var deletedPrice = await context.ArticlePriceLists.FindAsync(priceToRemove.Id); 

        // Assert
        Assert.True(removeResult);
        Assert.Null(deletedPrice); 
    }

}
