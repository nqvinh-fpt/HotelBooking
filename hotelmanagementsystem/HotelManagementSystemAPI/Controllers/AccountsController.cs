using BusinessObjects.Models;
using HotelManagementSystemAPI.RequestObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Abstractions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelManagementSystemAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("login-employee")]
        public string LoginEmployee([FromBody] LoginRequestObject loginRequest)
        {
            return _accountRepository.LoginEmployee(loginRequest.Username, loginRequest.Password);
        }

		[HttpPost("login-customer")]
		public string LoginCustomer([FromBody] LoginRequestObject loginRequest)
		{
			return _accountRepository.LoginCustomer(loginRequest.Username, loginRequest.Password);
		}

		[HttpPost("register-employee")]
        public IActionResult RegisterEmployee([FromBody] Employee employee)
        {
            try
            {
                _accountRepository.RegisterEmployee(employee);
                return Ok();
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("register-customer")]
        public IActionResult RegisterCustomer([FromBody] Customer customer)
        {
            try
            {
                _accountRepository.RegisterCustomer(customer);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
