using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Repositories.Dtos.AuthDto;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class TokenProvider
    {
        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles)
        {
            DateTime accessTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES_DEV"]));

            DateTime refreshTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["RefreshToken:EXPIRATION_MINUTES_DEV"]));

            string secretKey = _configuration["Jwt:Secret"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessTokenExpiration,
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            string accessToken = handler.CreateToken(tokenDescriptor);

            var authenticationResponse = new AuthenticationResponse
            {
                AccessToken = accessToken,
                AccessTokenExpiration = accessTokenExpiration,
                Email = user.Email,
                PersonName = user.FirstName + " " + user.LastName,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return authenticationResponse;
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
