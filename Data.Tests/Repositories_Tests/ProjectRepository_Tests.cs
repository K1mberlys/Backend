using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using Data.Tests.SeedData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Data.Tests.Repositories_Tests;

public class ProjectRepository_Tests
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
    private async Task SeedFullProjectDataAsync(DataContext context)
    {
        //nödvändig data för GetProjectListAsync
        context.Customers.AddRange(TestData.CustomerEntities);
        context.Users.AddRange(TestData.UserEntities);
        context.Statuses.AddRange(TestData.StatusTypeEntities);
        context.Projects.AddRange(TestData.ProjectEntities);
        context.ProjectArticles.AddRange(TestData.ProjectArticleEntities);
        context.Articles.AddRange(TestData.ArticleEntities);

        await context.SaveChangesAsync();
    }

    private async Task SeedDataWithProjects(DataContext context)
    {
        if (!context.Projects.Any())
        {
            await context.Projects.AddRangeAsync(TestData.ProjectEntities);
            await context.SaveChangesAsync();
        }
    }


    [Fact]
    public async Task AddAsync_ShouldAddProject()
    {
        // Arrange
        var context = GetDataContext();
        var repository = new ProjectRepository(context);

        var newProject = new ProjectEntity
        {
            ProjectName = "New Project",
            Description = "Test project",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            StatusTypeId = 1,
            CustomerId = 1,
            ProjectManagerId = 1
        };

        // Act
        var addResult = await repository.AddAsync(newProject);
        var savedProject = await context.Projects.FirstOrDefaultAsync(p => p.ProjectName == "New Project");

        // Assert
        Assert.True(addResult);
        Assert.NotNull(savedProject);
        Assert.Equal("New Project", savedProject!.ProjectName);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllProjects()
    {
        //Arrange
        var context = GetDataContext();
        await SeedDataWithProjects(context);

        IProjectRepository repository = new ProjectRepository(context);

        //Act
        var result = await repository.GetAllAsync();

        //Assert
        Assert.Equal(TestData.ProjectEntities.Length, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnProjectById()
    {
        // Arrange
        var context = GetDataContext();
        await SeedDataWithProjects(context);

        var repository = new ProjectRepository(context);
        var testProject = TestData.ProjectEntities[0];

        // Act
        var result = await repository.GetAsync(p => p.Id == testProject.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testProject.ProjectName, result!.ProjectName);
    }

    [Fact]
    public async Task GetProjectListAsync_ShouldReturnMappedProjects()
    {
        // Arrange
        var context = GetDataContext();

        
        await SeedFullProjectDataAsync(context);

        var repository = new ProjectRepository(context);

        // Act
        var result = await repository.GetProjectListAsync();


        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProject()
    {
        // Arrange
        var context = GetDataContext();
        await SeedDataWithProjects(context);

        var repository = new ProjectRepository(context);
        var projectToUpdate = await context.Projects.FirstOrDefaultAsync();
        Assert.NotNull(projectToUpdate);

        projectToUpdate!.ProjectName = "Updated Project";

        // Act
        var updateResult = await repository.UpdateAsync(projectToUpdate);
        var updatedProject = await context.Projects.FindAsync(projectToUpdate.Id);

        // Assert
        Assert.True(updateResult);
        Assert.NotNull(updatedProject);
        Assert.Equal("Updated Project", updatedProject!.ProjectName);
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveProject()
    {
        // Arrange
        var context = GetDataContext();
        await SeedDataWithProjects(context);

        var repository = new ProjectRepository(context);
        var projectToRemove = await context.Projects.FirstOrDefaultAsync();
        Assert.NotNull(projectToRemove);

        // Act
        var removeResult = await repository.RemoveAsync(projectToRemove);
        var deletedProject = await context.Projects.FindAsync(projectToRemove.Id);

        // Assert
        Assert.True(removeResult);
        Assert.Null(deletedProject);
    }

   
}
