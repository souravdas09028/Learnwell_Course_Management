using LearnWell.Application.Common.DTOs;

namespace LearnWell.Application.Services.Interface
{
    public interface IClassService
    {
        Task CreateAsync(CreateClassDto student);
        Task<IEnumerable<ClassDto>> GetAllAsync();
        Task<ClassDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, CreateClassDto staff);
        Task<bool> DeleteAsync(Guid id);
    }
}
