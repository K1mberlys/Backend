using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class PostalCodeService(IPostalCodeRepository repository) : IPostalCodeService
{
    private readonly IPostalCodeRepository _repository = repository;

    public async Task<IEnumerable<PostalCode>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(PostalCodeFactory.Map);
    }

    public async Task<PostalCode?> GetByIdAsync(string postalCodeId)
    {
        var entity = await _repository.GetAsync(pc => pc.PostalCode == postalCodeId);
        return entity != null ? PostalCodeFactory.Map(entity) : null;
    }

    public async Task<bool> CreateAsync(PostalCodeForm form)
    {
        var entity = PostalCodeFactory.Create(form);
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(string postalCodeId, PostalCodeForm form)
    {
        var existing = await _repository.GetAsync(pc => pc.PostalCode == postalCodeId);
        if (existing == null) return false;

        existing.City = form.City;

        return await _repository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(string postalCodeId)
    {
        var entity = await _repository.GetAsync(pc => pc.PostalCode == postalCodeId);
        return entity != null && await _repository.RemoveAsync(entity);
    }
}
