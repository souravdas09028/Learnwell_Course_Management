using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("course")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> EnrollInCourse([FromBody] EnrollCourseDto dto)
        {
            await _enrollmentService.EnrollStudentInCourseAsync(dto.StudentId, dto.CourseId, dto.StaffId);
            return Ok("Student enrolled in course and classes");
        }

        [HttpPost("class")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> EnrollInClass([FromBody] EnrollClassDto dto)
        {
            await _enrollmentService.EnrollStudentInClassAsync(dto.StudentId, dto.ClassId, dto.StaffId);
            return Ok("Student enrolled in class");
        }
    }
}
