using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Staff")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("course")]
        public async Task<IActionResult> EnrollInCourse(EnrollCourseDto dto)
        {
            await _enrollmentService.EnrollStudentInCourseAsync(dto.StudentId, dto.CourseId, dto.StaffId);
            return Ok("Student enrolled in course and its classes");
        }

        [HttpPost("class")]
        public async Task<IActionResult> EnrollInClass(EnrollClassDto dto)
        {
            await _enrollmentService.EnrollStudentInClassAsync(dto.StudentId, dto.ClassId, dto.StaffId);
            return Ok("Student enrolled in class");
        }
    }
}
