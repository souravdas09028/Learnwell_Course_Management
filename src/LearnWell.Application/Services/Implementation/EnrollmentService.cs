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
            var studentRepo = _unitOfWork.GetRepository<Student>();
            var classRepo = _unitOfWork.GetRepository<Class>();
            var staffRepo = _unitOfWork.GetRepository<Staff>();
            var studentClassRepo = _unitOfWork.GetRepository<StudentClass>();

            var student = await studentRepo.GetAsync(s => s.Id == studentId);
            var classEntity = await classRepo.GetAsync(c => c.Id == classId);
            var staff = await staffRepo.GetAsync(s => s.Id == staffId);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");
            if (classEntity == null)
                throw new KeyNotFoundException($"Class with ID {classId} not found.");
            if (staff == null)
                throw new KeyNotFoundException($"Staff with ID {staffId} not found.");

            //check if already enrolled
            var alreadyEnrolled = classEntity.Students.Any(sc => sc.StudentId == studentId);
            if (alreadyEnrolled)
                throw new InvalidOperationException("Student is already enrolled in this class.");

            var studentClass = new StudentClass
            {
                StudentId = studentId,
                ClassId = classId,
                AssignedByStaffId = staffId,
                AssignedAt = DateTime.UtcNow
            };

            await studentClassRepo.AddAsync(studentClass);
            await _unitOfWork.SaveAsync();
        }

        public async Task EnrollStudentInCourseAsync(Guid studentId, Guid courseId, Guid staffId)
        {
            var studentRepo = _unitOfWork.GetRepository<Student>();
            var courseRepo = _unitOfWork.GetRepository<Course>();
            var staffRepo = _unitOfWork.GetRepository<Staff>();
            var courseClassRepo = _unitOfWork.GetRepository<CourseClass>();
            var studentClassRepo = _unitOfWork.GetRepository<StudentClass>();
            var studentCourseRepo = _unitOfWork.GetRepository<StudentCourse>();

            var student = await studentRepo.GetAsync(s => s.Id == studentId);
            var course = await courseRepo.GetAsync(c => c.Id == courseId);
            var staff = await staffRepo.GetAsync(s => s.Id == staffId);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");
            if (course == null)
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");
            if (staff == null)
                throw new KeyNotFoundException($"Staff with ID {staffId} not found.");

            var alreadyEnrolledClass = await studentCourseRepo.GetAsync(
                    sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (alreadyEnrolledClass == null)
            {
                var studentCourse = new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    CreatedBy = staffId
                };

                await studentCourseRepo.AddAsync(studentCourse);
            }  

            //enroll in all classes linked to this course
            var courseClasses = await courseClassRepo.GetAllAsync(cc => cc.CourseId == courseId);

            foreach (var cc in courseClasses)
            {
                var alreadyEnrolled = await studentClassRepo.GetAsync(
                    sc => sc.StudentId == studentId && sc.ClassId == cc.ClassId);

                if (alreadyEnrolled == null)
                {
                    var studentClass = new StudentClass
                    {
                        StudentId = studentId,
                        ClassId = cc.ClassId,
                        AssignedByStaffId = staffId,
                        AssignedAt = DateTime.UtcNow
                    };

                    await studentClassRepo.AddAsync(studentClass);
                }
            }

            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Student {StudentId} enrolled in Course {CourseId} and all linked Classes by Staff {StaffId}",
                studentId, courseId, staffId);
        }

        public async Task AssignCourseToClassAsync(Guid courseId, Guid classId, Guid staffId)
        {
            var courseRepo = _unitOfWork.GetRepository<Course>();
            var classRepo = _unitOfWork.GetRepository<Class>();
            var courseClassRepo = _unitOfWork.GetRepository<CourseClass>();

            var course = await courseRepo.GetAsync(c => c.Id == courseId);
            var classEntity = await classRepo.GetAsync(c => c.Id == classId);

            if (course == null)
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");
            if (classEntity == null)
                throw new KeyNotFoundException($"Class with ID {classId} not found.");

            var alreadyLinked = await courseClassRepo.GetAsync(cc =>
                cc.CourseId == courseId && cc.ClassId == classId);

            if (alreadyLinked != null)
                throw new InvalidOperationException("This class is already linked to the course.");

            var courseClass = new CourseClass
            {
                CourseId = courseId,
                ClassId = classId,
                CreatedBy = staffId
            };

            await courseClassRepo.AddAsync(courseClass);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Class {ClassId} assigned to Course {CourseId}", classId, courseId);
        }
    }
}
