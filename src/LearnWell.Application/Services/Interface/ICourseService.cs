using LearnWell.Application.Common.DTOs;

namespace LearnWell.Application.Services.Interface
{
    public interface ICourseService
    {
        Task CreateAsync(CreateCourseDto student);
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, CreateCourseDto staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
