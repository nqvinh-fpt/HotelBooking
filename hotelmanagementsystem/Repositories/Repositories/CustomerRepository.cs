using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(HotelContext hotelContext) : base(hotelContext)
        {
        }

        // Create a new customer
        public async Task CreateAsync(Customer customer)
        {
            try
            {
                // Check if the username or email already exists
                bool usernameExists = await _hotelContext.Customers.AnyAsync(c => c.Username == customer.Username);
                bool emailExists = await _hotelContext.Customers.AnyAsync(c => c.Email == customer.Email);

                if (usernameExists)
                {
                    throw new Exception("Username already exists.");
                }
                if (emailExists)
                {
                    throw new Exception("Email already exists.");
                }
                customer.Password = HashPassword(customer.Password);
                await _hotelContext.Customers.AddAsync(customer);
                await _hotelContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while creating the customer.", ex);
            }
        }

        // Update an existing customer
        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                var existingCustomer = await _hotelContext.Customers.FindAsync(customer.CustomerId);
                customer.Password = existingCustomer.Password;
                if (existingCustomer != null)
                {
                    //customer.Password = HashPassword(customer.Password);
                    _hotelContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
                    await _hotelContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while updating the customer.", ex);
            }
        }


        // Delete a customer by ID
        public async Task DeleteAsync(int customerId)
        {
            try
            {
                var customer = await _hotelContext.Customers.FindAsync(customerId);
                if (customer != null)
                {
                    _hotelContext.Customers.Remove(customer);
                    await _hotelContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while deleting the customer.", ex);
            }
        }

        // Get all customers
        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                return await _hotelContext.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving customers.", ex);
            }
        }

        // Get a customer by ID
        public async Task<Customer> GetByIdAsync(int customerId)
        {
            try
            {
                var customer = await _hotelContext.Customers.FindAsync(customerId);
                return customer ?? throw new Exception("Customer not found.");
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while retrieving the customer.", ex);
            }
        }

        // Search customers by name (either first or last name)
        public async Task<List<Customer>> SearchByNameAsync(string name)
        {
            try
            {
                return await _hotelContext.Customers
                    .Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new Exception("An error occurred while searching for customers.", ex);
            }
        }
        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
