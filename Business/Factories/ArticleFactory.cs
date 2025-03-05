using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class ArticleFactory
{
    public static ArticleEntity Create(ArticleRegistrationForm form) => new()
    {
        ArticleName = form.ArticleName,
        Description = form.Description,
        IsProduct = form.IsProduct
    };

    public static ArticleEntity Create(ArticleUpdateForm form) => new()
    {
        Id = form.Id, 
        ArticleName = form.ArticleName,
        Description = form.Description,
        IsProduct = form.IsProduct
    };

    public static Article Map(ArticleEntity entity) => new()
    {
        Id = entity.Id,
        ArticleName = entity.ArticleName,
        Description = entity.Description,
        IsProduct = entity.IsProduct
    };
}
