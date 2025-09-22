using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim not found.");

            Guid staffId = Guid.Parse(userIdClaim.Value);

            await _enrollmentService.EnrollStudentInCourseAsync(dto.StudentId, dto.CourseId, staffId);
            return Ok("Student enrolled in course and its classes");
        }

        [HttpPost("class")]
        public async Task<IActionResult> EnrollInClass(EnrollClassDto dto)
        {
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim not found.");

            Guid staffId = Guid.Parse(userIdClaim.Value);

            await _enrollmentService.EnrollStudentInClassAsync(dto.StudentId, dto.ClassId, staffId);
            return Ok("Student enrolled in class");
        }

        [HttpPost("coursetoclass")]
        public async Task<IActionResult> AssignCourseToClass(EnrollCourseInClass dto)
        {
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID claim not found.");

            Guid staffId = Guid.Parse(userIdClaim.Value);

            await _enrollmentService.AssignCourseToClassAsync(dto.CourseId, dto.ClassId, staffId);
            return Ok("Student enrolled in class");
        }
    }
}
