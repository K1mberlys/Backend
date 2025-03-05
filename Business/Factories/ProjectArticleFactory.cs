using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class ProjectArticleFactory
{
    public static ProjectArticleEntity Create(ProjectArticleForm form) => new()
    {
        ProjectId = form.ProjectId,
        ArticleId = form.ArticleId,
        Quantity = form.Quantity
    };

    public static ProjectArticle Map(ProjectArticleEntity entity) => new()
    {
        Id = entity.Id,
        ProjectId = entity.ProjectId,
        ProjectName = entity.Project.ProjectName,
        ArticleId = entity.ArticleId, 
        ArticleName = entity.Article.ArticleName,
        Quantity = entity.Quantity
    };
}
