using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
using Microsoft.Extensions.Logging;

namespace LearnWell.Application.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CourseService> _logger;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CourseService> logger) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task CreateAsync(CreateCourseDto student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, CreateCourseDto staff)
        {
            throw new NotImplementedException();
        }
    }
}
