using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Staff")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
        {
            await _courseService.CreateAsync(dto);
            //return Ok(course);

            return CreatedAtAction(nameof(GetCourse), new { id = 0 }, dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(Guid id)
        {
            var course = await _courseService.GetAsync(id);
            return course == null ? NotFound() : Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, CreateCourseDto dto)
        {
            var course = await _courseService.GetAsync(id);
            if (course == null) return NotFound();

            await _courseService.UpdateAsync(id, dto);
            return Ok(course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _courseService.GetAsync(id);
            if (course == null) return NotFound();

            await _courseService.DeleteAsync(id);
            return Ok("Deleted");
        }
    }  
}
