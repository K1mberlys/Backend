using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        return entities.Select(CustomerFactory.Map);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var entity = await _customerRepository.GetAsync(c => c.Id == id);
        return entity != null ? CustomerFactory.Map(entity) : null;
    }

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var entity = CustomerFactory.Create(form);
        return await _customerRepository.AddAsync(entity);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var entity = await _customerRepository.GetAsync(c => c.Id == id);
        return entity != null && await _customerRepository.RemoveAsync(entity);
    }
}



