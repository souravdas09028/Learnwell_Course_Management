using LearnWell.Application.Common.DTOs;

namespace LearnWell.Application.Services.Interface
{
    public interface IClassService
    {
        Task<ClassDto> CreateAsync(CreateClassDto student, Guid createdBy);
        Task<IEnumerable<ClassDto>> GetAllAsync();
        Task<ClassDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, ClassDto staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
