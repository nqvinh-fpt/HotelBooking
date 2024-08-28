using BusinessObjects.Models;

namespace Repositories.Abstractions
{
    public interface ICustomerRepository
    {
        Task CreateAsync(Customer customer);
        Task DeleteAsync(int customerId);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int customerId);
        Task<List<Customer>> SearchByNameAsync(string name);
        Task UpdateAsync(Customer customer);
    }
}