using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;
public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task<IEnumerable<StatusType?>> GetStatusTypesAsync()
    {
        var entities = await _statusTypeRepository.GetAllAsync();
        return entities.Select(StatusTypeFactory.Map);
    }
}
