
using HotelManagementSystemClient.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StoreManagementClient.Helpers
{
    public class JwtHelper
    {
        public static TokenResponse DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var tokenResponse = new TokenResponse
            {
                CustomerId = int.Parse(jsonToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value),
                Email = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "email").Value,
                Username = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value,
                Role = int.Parse(jsonToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value)
            };

            return tokenResponse;
        }
    }
}

