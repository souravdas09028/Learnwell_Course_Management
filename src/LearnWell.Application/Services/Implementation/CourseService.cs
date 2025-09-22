using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
using LearnWell.Domain.Entities;
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

        public async Task<CourseDto> CreateAsync(CreateCourseDto courseDto)
        {
            try
            {
                var courseEntity = _mapper.Map<Course>(courseDto);

                await _unitOfWork.GetRepository<Course>().AddAsync(courseEntity);
                await _unitOfWork.SaveAsync();

                var resultDto = _mapper.Map<CourseDto>(courseEntity);

                return resultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                _logger.LogError(ex, "Failed to save course: {Username}", courseDto.Name);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid courseId)
        {
            var courseRepo = _unitOfWork.GetRepository<Course>();
            var courseEntity = await courseRepo.GetAsync(c => c.Id == courseId);

            if (courseEntity == null)
            {
                _logger.LogWarning("Attempted to delete class with ID {CourseId}, but it was not found.", courseId);
                return false;
            }

            await courseRepo.DeleteAsync(courseEntity);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Course with ID {CourseId} deleted successfully.", courseId);
            return true;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _unitOfWork.GetRepository<Course>().GetAllAsync(filter: null);
            var courseDTOs = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return courseDTOs;
        }

        public async Task<CourseDto> GetAsync(Guid courseId)
        {
            var courseObj = await _unitOfWork.GetRepository<Course>().GetAsync(filter: s => s.Id == courseId);
            var courseDTO = _mapper.Map<CourseDto>(courseObj);

            return courseDTO;
        }

        public async Task UpdateAsync(Guid userId, CourseDto courseDto)
        {
            var existingCourse = await _unitOfWork.GetRepository<Course>().GetAsync(filter: s => s.Id == courseDto.Id);

            if (existingCourse == null)
                throw new KeyNotFoundException($"Class with ID {courseDto.Id} not found.");

            existingCourse.Name = courseDto.Name;
            existingCourse.UpdateAuditFields(userId);

            await _unitOfWork.GetRepository<Course>().UpdateAsync(existingCourse);
            await _unitOfWork.SaveAsync();
        }
    }
}
