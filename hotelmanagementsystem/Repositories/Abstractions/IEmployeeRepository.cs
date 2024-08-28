using BusinessObjects.Models;

namespace Repositories.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> SearchEmployeesAsync(string firstName = null, string lastName = null, string email = null, string position = null, string department = null);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
    }
}