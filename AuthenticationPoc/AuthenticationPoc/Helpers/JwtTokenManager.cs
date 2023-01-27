using AuthenticationPoc.DataTransferObjects;
using AuthenticationPoc.Interfaces.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationPoc.Helpers
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(UserDto userDto)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, userDto.Email)
            };

            var jwtKey = _configuration.GetSection("JwtSettings:SecurityKey").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenExpiringTime = Double.Parse(_configuration.GetSection("JwtSettings:TokenExpiringTimeInHours").Value);
            
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(tokenExpiringTime),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
