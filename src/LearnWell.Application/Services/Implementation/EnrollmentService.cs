using AutoMapper;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
using LearnWell.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace LearnWell.Application.Services.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CourseService> _logger;

        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CourseService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task EnrollStudentInClassAsync(Guid studentId, Guid classId, Guid staffId)
        {
            throw new NotImplementedException();
        }

        public async Task EnrollStudentInCourseAsync(Guid studentId, Guid courseId, Guid staffId)
        {
            //var course = await _unitOfWork.Courses.Query()
            //    .Include(c => c.CourseClasses)
            //    .FirstOrDefaultAsync(c => c.Id == courseId);

            //if (course == null) throw new Exception("Course not found");

            //await _unitOfWork.StudentCourses.AddAsync(new StudentCourse
            //{
            //    StudentId = studentId,
            //    CourseId = courseId
            //});

            //foreach (var cc in course.CourseClasses)
            //{
            //    await _unitOfWork.StudentClasses.AddAsync(new StudentClass
            //    {
            //        StudentId = studentId,
            //        ClassId = cc.ClassId,
            //        AssignedByStaffId = staffId,
            //        AssignedAt = DateTime.UtcNow
            //    });
            //}

            //await _unitOfWork.SaveChangesAsync();
        }
    }
}
