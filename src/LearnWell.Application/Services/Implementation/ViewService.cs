using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
using LearnWell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnWell.Application.Services.Implementation
{
    public class ViewService : IViewService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ViewService> _logger;
        public ViewService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ViewService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ClassDto>> GetClassesInCourse(Guid courseId)
        {
            var courseClassRepo = _unitOfWork.GetRepository<CourseClass>();
            var classRepo = _unitOfWork.GetRepository<Class>();

            var classIds = (await courseClassRepo.GetAllAsync(cc => cc.CourseId == courseId))
                .Select(cc => cc.ClassId)
                .ToList();

            var classes = await classRepo.GetAllAsync(c => classIds.Contains(c.Id));
            return _mapper.Map<IEnumerable<ClassDto>>(classes);
        }

        public async Task<IEnumerable<StudentDto>> GetClassmates(Guid classId)
        {
            var classEntity = await _unitOfWork.GetRepository<Class>()
                    .GetAsync(
                        filter: c => c.Id == classId,
                        include: query => query.Include(c => c.Students)
                    );

            if (classEntity == null)
                throw new KeyNotFoundException($"Class with ID {classId} not found.");

            return _mapper.Map<IEnumerable<StudentDto>>(classEntity.Students);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesForClass(Guid classId)
        {
            var courseClassRepo = _unitOfWork.GetRepository<CourseClass>();
            var courseRepo = _unitOfWork.GetRepository<Course>();

            var courseIds = (await courseClassRepo.GetAllAsync(cc => cc.ClassId == classId))
                .Select(cc => cc.CourseId)
                .ToList();

            var courses = await courseRepo.GetAllAsync(c => courseIds.Contains(c.Id));
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsInClass(Guid classId)
        {
            var classEntity = await _unitOfWork.GetRepository<Class>()
                .GetAsync(
                    filter: c => c.Id == classId,
                    include: query => query
                        .Include(c => c.Students)
                        .ThenInclude(sc => sc.Student)
                );

            if (classEntity == null)
                throw new KeyNotFoundException($"Class with ID {classId} not found.");

            var studentDtos = classEntity.Students
                .Select(sc => _mapper.Map<StudentDto>(sc.Student))
                .ToList();

            return studentDtos;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsInCourse(Guid courseId)
        {
            var courseEntity = await _unitOfWork.GetRepository<Course>()
                .GetAsync(
                    filter: c => c.Id == courseId,
                    include: query => query
                        .Include(c => c.Students)
                        .ThenInclude(sc => sc.Student)
                );

            if (courseEntity == null)
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");

            var studentDtos = courseEntity.Students
                .Select(sc => _mapper.Map<StudentDto>(sc.Student))
                .ToList();

            return studentDtos;

        }
    }
}
