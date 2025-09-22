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
                _logger.LogError(ex, "Something went wrong");
                _logger.LogError(ex, "Failed to register student: {Username}", studentDto.Username);
                throw;
            }
        }

        public async Task RegisterStaffAsync(RegisterStaffDto staffDto)
        {
            try
            {
                var hashedPassword = _passwordHasher.HashPassword(staffDto.Password);
                var staff = new Staff(staffDto.FullName, staffDto.Username, hashedPassword);

                await _unitOfWork.GetRepository<Staff>().AddAsync(staff);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");
                _logger.LogError(ex, "Failed to register student: {Username}", staffDto.Username);
                throw;
            }
        }

    }
}
