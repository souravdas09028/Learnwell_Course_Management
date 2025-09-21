using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
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

        public Task CreateAsync(CreateClassDto student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClassDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ClassDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, CreateClassDto staff)
        {
            throw new NotImplementedException();
        }
    }
}
