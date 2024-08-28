using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(HotelContext hotelContext) : base(hotelContext)
        {
        }

        // Get all employees
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _hotelContext.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all employees: {ex.Message}");
            }
        }

        // Get employee by ID
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                return await _hotelContext.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving employee with ID {employeeId}: {ex.Message}");
            }
        }

        // Add a new employee
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            try
            {
                _hotelContext.Employees.Add(employee);
                await _hotelContext.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding employee: {ex.Message}");
            }
        }

        // Update an existing employee
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _hotelContext.Employees.Update(employee);
                await _hotelContext.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating employee with ID {employee.EmployeeId}: {ex.Message}");
            }
        }

        // Delete an employee
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(employeeId);
                if (employee != null)
                {
                    _hotelContext.Employees.Remove(employee);
                    await _hotelContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Employee with ID {employeeId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting employee with ID {employeeId}: {ex.Message}");
            }
        }

        // Search employees with filters
        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(string firstName = null, string lastName = null, string email = null, string position = null, string department = null)
        {
            try
            {
                var query = _hotelContext.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(firstName))
                {
                    query = query.Where(e => e.FirstName.Contains(firstName));
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    query = query.Where(e => e.LastName.Contains(lastName));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(e => e.Email.Contains(email));
                }

                if (!string.IsNullOrEmpty(position))
                {
                    query = query.Where(e => e.Position.Contains(position));
                }

                if (!string.IsNullOrEmpty(department))
                {
                    query = query.Where(e => e.Department.Contains(department));
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching employees: {ex.Message}");
            }
        }
    }
}
