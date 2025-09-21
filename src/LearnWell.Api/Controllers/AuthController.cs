using LearnWell.Application.Common.DTOs;
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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var (role, isValid) = await _authService.ValidateCredentialsAsync(request.Username, request.Password);

            if (!isValid)
                return Unauthorized("Invalid username or password");
            
            var token = _authService.GenerateToken(request.Username, role);

            return Ok(new { Token = token.Token, Expiration = token.Expiration });
        }
    }
}
