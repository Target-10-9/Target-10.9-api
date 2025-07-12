using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Target10._9.Business.Providers;

namespace Target10._9_api.Providers;

public class JwtProvider(
        IConfiguration configuration
) : IJwtProvider
{
        public string GenerateToken(string email, string userId)
        { 
                var claims = new[]
                {
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.NameIdentifier, userId)
                };
                                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
        }
}
