namespace LearnWell.Application.Services.Interface
{
    public interface IEnrollmentService
    {
        Task EnrollStudentInCourseAsync(Guid studentId, Guid courseId, Guid staffId);
        Task EnrollStudentInClassAsync(Guid studentId, Guid classId, Guid staffId);
    }
}
