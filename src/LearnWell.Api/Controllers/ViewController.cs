using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ViewController : ControllerBase
    {
        private readonly IViewService _viewService;

        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }

        [HttpGet("course/{courseId}/students")]
        public async Task<IActionResult> GetStudentsInCourse(Guid courseId)
        {
            var students = await _viewService.GetStudentsInCourse(courseId);

            return Ok(students);
        }

        [HttpGet("class/{classId}/students")]
        public async Task<IActionResult> GetStudentsInClass(Guid classId)
        {
            var students = await _viewService.GetStudentsInClass(classId);

            return Ok(students);
        }

        [HttpGet("course/{courseId}/classes")]
        public async Task<IActionResult> GetClassesInCourse(Guid courseId)
        {
            var classes = await _viewService.GetClassesInCourse(courseId);

            return Ok(classes);
        }

        [HttpGet("class/{classId}/courses")]
        public async Task<IActionResult> GetCoursesForClass(Guid classId)
        {
            var courses = await _viewService.GetCoursesForClass(classId);

            return Ok(courses);
        }

        [HttpGet("class/{classId}/classmates")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetClassmates(Guid classId)
        {
            var classmates = await _viewService.GetClassmates(classId);

            return Ok(classmates);
        }
    }
}
