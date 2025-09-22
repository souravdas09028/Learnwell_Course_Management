using LearnWell.Application.Common.DTOs;

namespace LearnWell.Application.Services.Interface
{
    public interface ICourseService
    {
        Task<CourseDto> CreateAsync(CreateCourseDto courseDto);
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto> GetAsync(Guid id);
        Task UpdateAsync(Guid userId, CourseDto courseDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
