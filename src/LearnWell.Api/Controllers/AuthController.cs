using LearnWell.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Replace with real user validation
            if (request.Username == "staff" && request.Password == "mypassword")
            {
                var token = _authService.GenerateToken(request.Username, "Staff");
                return Ok(new { token });
            }

            if (request.Username == "student" && request.Password == "password")
            {
                var token = _authService.GenerateToken(request.Username, "Student");
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password");
        }
    }
    public record LoginRequest(string Username, string Password);
}
