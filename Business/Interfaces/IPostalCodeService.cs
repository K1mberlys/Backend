using Business.Models;

namespace Business.Interfaces
{
    public interface IPostalCodeService
    {
        Task<IEnumerable<PostalCode>> GetAllAsync();
        Task<PostalCode?> GetByIdAsync(string postalCodeId);
        Task<bool> CreateAsync(PostalCodeForm form);
        Task<bool> UpdateAsync(string postalCodeId, PostalCodeForm form);
        Task<bool> DeleteAsync(string postalCodeId);
    }
}