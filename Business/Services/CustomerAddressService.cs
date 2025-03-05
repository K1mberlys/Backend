using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class CustomerAddressService(ICustomerAddressRepository repository) : ICustomerAddressService
{
    private readonly ICustomerAddressRepository _repository = repository;

    public async Task<IEnumerable<CustomerAddress>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(CustomerAddressFactory.Map);
    }

    public async Task<CustomerAddress?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetAsync(ca => ca.Id == id);
        return entity != null ? CustomerAddressFactory.Map(entity) : null;
    }

    public async Task<bool> CreateAsync(CustomerAddressForm form)
    {
        var entity = CustomerAddressFactory.Create(form);
        return await _repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(int id, CustomerAddressForm form)
    {
        var existing = await _repository.GetAsync(ca => ca.Id == id);
        if (existing == null) return false;

        existing.CustomerId = form.CustomerId;
        existing.AddressTypeId = form.AddressTypeId;
        existing.AddressLine_1 = form.AddressLine1;
        existing.AddressLine_2 = form.AddressLine2;
        existing.PostalCodeId = form.PostalCode;

        return await _repository.UpdateAsync(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetAsync(ca => ca.Id == id);
        return entity != null && await _repository.RemoveAsync(entity);
    }
}
