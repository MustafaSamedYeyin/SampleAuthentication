using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SampleAuthentication.Helper
{
    public class JwtHelper
    {
        public string GenerateToken()
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ThisIsASecretKeyForJwtTokenGenerationAndOnlyYouShouldKnotIt"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: new[]
                {
                    new System.Security.Claims.Claim("userName", "Mustafa Samed")
                },
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials));

            return token;
        }
    }
}
