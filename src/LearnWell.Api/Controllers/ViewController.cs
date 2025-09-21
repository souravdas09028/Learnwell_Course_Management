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

            //_unitOfWork.StudentCourses.Query()
            //    .Where(sc => sc.CourseId == courseId)
            //    .Select(sc => sc.Student)
            //    .ToListAsync();

            return Ok(students);
        }

        [HttpGet("class/{classId}/students")]
        public async Task<IActionResult> GetStudentsInClass(Guid classId)
        {
            var students = await _viewService.GetStudentsInClass(classId);

            //_unitOfWork.StudentClasses.Query()
            //.Where(sc => sc.ClassId == classId)
            //.Select(sc => sc.Student)
            //.ToListAsync();

            return Ok(students);
        }

        [HttpGet("course/{courseId}/classes")]
        public async Task<IActionResult> GetClassesInCourse(Guid courseId)
        {
            var classes = await _viewService.GetClassesInCourse(courseId);

            //_unitOfWork.CourseClasses.Query()
            //.Where(cc => cc.CourseId == courseId)
            //.Select(cc => cc.Class)
            //.ToListAsync();

            return Ok(classes);
        }

        [HttpGet("class/{classId}/courses")]
        public async Task<IActionResult> GetCoursesForClass(Guid classId)
        {
            var courses = await _viewService.GetCoursesForClass(classId);

            //_unitOfWork.CourseClasses.Query()
            //.Where(cc => cc.ClassId == classId)
            //.Select(cc => cc.Course)
            //.ToListAsync();

            return Ok(courses);
        }

        [HttpGet("class/{classId}/classmates")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetClassmates(Guid classId)
        {
            var classmates = await _viewService.GetClassmates(classId);

            //_unitOfWork.StudentClasses.Query()
            //.Where(sc => sc.ClassId == classId)
            //.Select(sc => sc.Student)
            //.ToListAsync();

            return Ok(classmates);
        }
    }
}
