using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
using LearnWell.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace LearnWell.Application.Services.Implementation
{
    public class ClassService : IClassService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClassService> _logger;
        public ClassService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ClassService> logger) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ClassDto> CreateAsync(CreateClassDto classDto, Guid createdBy)
        {
            try
            {
                var classEntity = new Class(classDto.Name, createdBy);

                await _unitOfWork.GetRepository<Class>().AddAsync(classEntity);
                await _unitOfWork.SaveAsync();

                var resultDto = _mapper.Map<ClassDto>(classEntity);

                return resultDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                _logger.LogError(ex, "Failed to save class: {Username}", classDto.Name);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid classId)
        {
            var classRepo = _unitOfWork.GetRepository<Class>();
            var classEntity = await classRepo.GetAsync(c => c.Id == classId);

            if (classEntity == null)
            {
                _logger.LogWarning("Attempted to delete class with ID {ClassId}, but it was not found.", classId);
                return false;
            }

            await classRepo.DeleteAsync(classEntity);
            await _unitOfWork.SaveAsync();

            _logger.LogInformation("Class with ID {ClassId} deleted successfully.", classId);
            return true;
        }

        public async Task<IEnumerable<ClassDto>> GetAllAsync()
        {
            var classes = await _unitOfWork.GetRepository<Class>().GetAllAsync(filter: null);
            var classDTOs = _mapper.Map<IEnumerable<ClassDto>>(classes);

            return classDTOs;
        }

        public async Task<ClassDto> GetAsync(Guid classId)
        {
            var classObj = await _unitOfWork.GetRepository<Class>().GetAsync(filter: s => s.Id == classId);
            var classDTO = _mapper.Map<ClassDto>(classObj);

            return classDTO;
        }

        public async Task UpdateAsync(Guid userId, ClassDto classDto)
        {
            var existingClass = await _unitOfWork.GetRepository<Class>().GetAsync(filter: s => s.Id == classDto.Id);
            
            if (existingClass == null)
                throw new KeyNotFoundException($"Class with ID {classDto.Id} not found.");

            existingClass.UpdateName(classDto.Name);
            existingClass.UpdateAuditFields(userId);

            await _unitOfWork.GetRepository<Class>().UpdateAsync(existingClass);
            await _unitOfWork.SaveAsync();
        }
    }
}
