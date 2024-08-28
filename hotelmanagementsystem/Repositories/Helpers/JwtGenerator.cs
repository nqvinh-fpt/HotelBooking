using BusinessObjects.Configurations;
using BusinessObjects.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementSystemAPI.Helpers
{
    public class JwtGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtGenerator(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        internal string GenerateJwtToken(Employee employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.EmployeeId.ToString()),
                    new Claim(ClaimTypes.Email, employee.Email.ToString()),
                    new Claim(ClaimTypes.Name, employee.Username.ToString()),
                    new Claim(ClaimTypes.Role, employee.Role.ToString()),
                    // Add other claims as needed
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Lifespan),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        internal string GenerateJwtToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Email, customer.Email.ToString()),
                    new Claim(ClaimTypes.Name, customer.Username.ToString()),
                    new Claim(ClaimTypes.Role, "2"),
                    // Add other claims as needed
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.Lifespan),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
