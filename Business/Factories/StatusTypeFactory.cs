using Business.Models;
using Data.Enteties;
using System.Data;

namespace Business.Factories;

public static class StatusTypeFactory
{

    public static StatusType? Map(StatusTypeEntity entity) => entity == null ? null : new StatusType
    {
        Id = entity.Id,
        StatusTypeName = entity.StatusType
    };
}
