using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SampleAuthentication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetToken()
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
            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetData() 
        {
            return Ok("This is proctected data.");
        }
    }
}
