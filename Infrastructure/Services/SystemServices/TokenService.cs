
using Core.DomainModels;
using Core.Interfaces.IServices.SystemIServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Services.SystemServices

{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ClaimService _claimService;
        public TokenService(IConfiguration configuration, ClaimService claimService)
        {
            _configuration = configuration;
            _claimService = claimService;
        }


        public async Task<string> CreateTokenForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);
            var claimsIdentity = await _claimService.CreateClaimsIdentityForUserAsync(user); // Await the Task result
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public DateTime? ExtractValidToDateFromToken(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;
                if (jwtToken != null)
                {
                    return jwtToken.ValidTo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JWT token: {ex.Message}");
            }
            return null;
        }

    }
}
