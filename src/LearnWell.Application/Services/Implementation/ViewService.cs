using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
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

        public Task<IEnumerable<ClassDto>> GetClassesInCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDto>> GetClassmates(Guid classId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseDto>> GetCoursesForClass(Guid classId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDto>> GetStudentsInClass(Guid classId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDto>> GetStudentsInCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }
    }
}
