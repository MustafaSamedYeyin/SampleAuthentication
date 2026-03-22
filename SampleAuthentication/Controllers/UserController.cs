using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleAuthentication.Helper;
using System.IdentityModel.Tokens.Jwt;

namespace SampleAuthentication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public JwtHelper _jwtHelper { get; set; }
        public UserController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [HttpGet]
        public IActionResult GetToken()
        {
            return Ok(_jwtHelper.GenerateToken());
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetData() 
        {
            return Ok("This is proctected data.");
        }
    }
}
