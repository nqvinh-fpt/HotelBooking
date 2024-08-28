using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Repositories.Abstractions;

namespace HotelManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        //[Authorize(Roles = "0")]
        // Create a new customer
        [HttpPost("save-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                await _customerRepository.CreateAsync(customer);
                return Ok("Customer created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update an existing customer
        [HttpPut("edit-customer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                customer.CustomerId = id;
                await _customerRepository.UpdateAsync(customer);
                return Ok("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete a customer by ID
        [HttpDelete("remove-customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerRepository.DeleteAsync(id);
                return Ok("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get all customers
        [HttpGet("get-all-customer")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get a customer by ID
        [HttpGet("get-customer-by-id/{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Search customers by name (either first or last name)
        [HttpGet("search-customer")]
        public async Task<ActionResult<List<Customer>>> SearchCustomersByName([FromQuery] string name)
        {
            try
            {
                var customers = await _customerRepository.SearchByNameAsync(name);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
