using Business.Interfaces;
using Data.Interfaces;
using Business.Factories;
using Business.Models;

namespace Business.Services;

public class UserAddressService(IUserAddressRepository repository) : IUserAddressService
{
    private readonly IUserAddressRepository _repository = repository;

    public async Task<IEnumerable<UserAddress>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(UserAddressFactory.Map);
    }

    public async Task<UserAddress?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetAsync(ua => ua.Id == id);
        return entity != null ? UserAddressFactory.Map(entity) : null;
    }

    public async Task<bool> CreateAsync(UserAddressForm form)
    {
        var entity = UserAddressFactory.Create(form);
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(int id, UserAddressForm form)
    {
        var existing = await _repository.GetAsync(ua => ua.Id == id);
        if (existing == null) return false;

        existing.AddressTypeId = form.AddressTypeId;
        existing.AddressLine_1 = form.AddressLine_1;
        existing.AddressLine_2 = form.AddressLine_2;
        existing.PostalCodeId = form.PostalCodeId;

        return await _repository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetAsync(ua => ua.Id == id);
        return entity != null && await _repository.RemoveAsync(entity);
    }
}

