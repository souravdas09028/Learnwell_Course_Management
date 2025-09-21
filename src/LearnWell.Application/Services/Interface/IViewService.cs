using LearnWell.Application.Common.DTOs;

namespace LearnWell.Application.Services.Interface
{
    public interface IViewService
    {
        Task<IEnumerable<StudentDto>> GetStudentsInCourse(Guid courseId);
        Task<IEnumerable<StudentDto>> GetStudentsInClass(Guid classId);
        Task<IEnumerable<ClassDto>> GetClassesInCourse(Guid courseId);
        Task<IEnumerable<CourseDto>> GetCoursesForClass(Guid classId);
        Task<IEnumerable<StudentDto>> GetClassmates(Guid classId);
    }
}
