using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
using LearnWell.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace LearnWell.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger, IPasswordHasher passwordHasher) 
        {            
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task RegisterStudentsAsync(RegisterStudentDto studentDto)
        {
            try
            {
                var hashedPassword = _passwordHasher.HashPassword(studentDto.Password);
                var student = new Student(studentDto.FullName, studentDto.Username, hashedPassword);

                await _unitOfWork.GetRepository<Student>().AddAsync(student);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogError(ex, "Failed to create SKU");
                throw;
            }
        }

        public async Task RegisterStaffAsync(RegisterStaffDto dto)
        {
            try
            {
                var hashedPassword = _passwordHasher.HashPassword(dto.Password);
                var staff = new Staff(dto.FullName, dto.Username, hashedPassword);

                await _unitOfWork.GetRepository<Staff>().AddAsync(staff);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register staff");
                throw;
            }
        }

    }
}
