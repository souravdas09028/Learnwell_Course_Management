using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Implementation;
using LearnWell.Application.Services.Interface;
using LearnWell.Domain.Entities;
using LearnWell.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearnWell.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher _passwordHasher = new PasswordHasher();

        public AuthService(IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork,
            ILogger<UserService> logger, IPasswordHasher passwordHasher)
        {
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public JwtTokenResponse GenerateToken(Guid userId, string username, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiryMinutes)),
                signingCredentials: creds);

            return new JwtTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public async Task<(Guid userId, string Role, bool IsValid)> ValidateCredentialsAsync(string username, string password)
        {
            var staff = await _unitOfWork.GetRepository<Staff>().GetAsync(s => s.Username == username);

            if (staff != null)
            {
                if (_passwordHasher.VerifyPassword(staff.HashedPassword, password))
                {
                    return (staff.Id, "Staff", true);
                }

                _logger.LogWarning("Invalid password for staff: {Username}", username);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var student = await _unitOfWork.GetRepository<Student>().GetAsync(s => s.Username == username);

            if (student != null)
            {
                if (_passwordHasher.VerifyPassword(student.HashedPassword, password))
                {
                    return (student.Id, "Student", true);
                }

                _logger.LogWarning("Invalid password for student: {Username}", username);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            _logger.LogInformation("No user found with username: {Username}", username);
            throw new UnauthorizedAccessException("Invalid username or password");
        }
    }
}
