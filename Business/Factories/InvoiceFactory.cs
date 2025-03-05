using Business.Models;
using Data.Enteties;

namespace Business.Factories;

public static class InvoiceFactory
{
    public static InvoiceEntity Create(InvoiceRegistrationForm form) => new()
    {
        ProjectId = form.ProjectId,
        CustomerId = form.CustomerId,
        TotalAmount = form.TotalAmount,
        IssueDate = form.IssueDate,
        DueDate = form.DueDate,
        IsPaid = form.IsPaid
    };

    public static Invoice Map(InvoiceEntity entity) => new()
    {
        Id = entity.Id,
        ProjectId = entity.ProjectId,
        ProjectName = entity.Project.ProjectName,
        CustomerId = entity.CustomerId,
        CustomerName = entity.Customer.CustomerName,
        TotalAmount = entity.TotalAmount,
        IssueDate = entity.IssueDate,
        DueDate = entity.DueDate,
        IsPaid = entity.IsPaid
    };
}
