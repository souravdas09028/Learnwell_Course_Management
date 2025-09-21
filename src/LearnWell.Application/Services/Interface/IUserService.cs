using LearnWell.Application.Common.DTOs;

namespace LearnWell.Application.Services.Interface
{
    public interface IUserService
    {
        Task RegisterStudentsAsync(RegisterStudentDto student);
        Task RegisterStaffAsync(RegisterStaffDto staff);
    }
}
