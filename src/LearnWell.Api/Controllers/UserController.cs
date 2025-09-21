using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register/student")]
        public async Task<IActionResult> RegisterStudent(RegisterStudentDto dto)
        {
            await _userService.RegisterStudentsAsync(dto);

            return Ok("Student registered");
        }

        [HttpPost("register/staff")]
        public async Task<IActionResult> RegisterStaff(RegisterStaffDto dto)
        {
            await _userService.RegisterStaffAsync(dto);

            return Ok("Staff registered");
        }
    }
}
