using BusinessObjects.Configurations;
using BusinessObjects.Models;
using HotelManagementSystemAPI.Helpers;
using Microsoft.IdentityModel.Tokens;
using Repositories.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repositories.Repositories
{
	public sealed class AccountRepository : BaseRepository, IAccountRepository
	{
		private readonly JwtGenerator _jwtGenerator;
		public AccountRepository(HotelContext hotelContext, JwtGenerator jwtGenerator) : base(hotelContext)
		{
			_jwtGenerator = jwtGenerator;
		}

		public string LoginEmployee(string username, string password)
		{
			var employee = _hotelContext.Employees.FirstOrDefault(e => e.Username == username);
			string token = "";

			if (employee == null || !VerifyPasswordHash(password, employee.Password))
			{
				throw new UnauthorizedAccessException("Invalid username or password.");
			}

			token = _jwtGenerator.GenerateJwtToken(employee);
			return token;
		}

		public string LoginCustomer(string username, string password)
		{
			var customer = _hotelContext.Customers.FirstOrDefault(e => e.Username.Equals(username));
			string token = "";
			if (customer == null || !VerifyPasswordHash(password, customer.Password))
			{
				throw new UnauthorizedAccessException("Invalid username or password.");
			}
			token = _jwtGenerator.GenerateJwtToken(customer);
			return token;
		}

		public void RegisterEmployee(Employee employee)
		{
			try
			{
				if (_hotelContext.Employees.Any(e => e.Email == employee.Email))
				{
					throw new InvalidOperationException("Employee with this email already exists.");
				}

				employee.Password = HashPassword(employee.Password);
				_hotelContext.Employees.Add(employee);
				_hotelContext.SaveChanges();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        public void RegisterCustomer(Customer customer)
        {
            try
            {
                if (_hotelContext.Customers.Any(e => e.Email == customer.Email))
                {
                    throw new InvalidOperationException("Employee with this email already exists.");
                }
                if (_hotelContext.Customers.Any(e => e.Username == customer.Username))
                {
                    throw new InvalidOperationException("Employee with this username already exists.");
                }
                customer.Password = HashPassword(customer.Password);
                _hotelContext.Customers.Add(customer);
                _hotelContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

		private bool VerifyPasswordHash(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);


	}
}
