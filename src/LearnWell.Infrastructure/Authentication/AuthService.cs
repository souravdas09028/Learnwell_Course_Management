using LearnWell.Application.Common.Interfaces;
using LearnWell.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        private readonly LearnWellDbContext _context;

        public AuthService(IOptions<JwtSettings> jwtSettings, LearnWellDbContext context)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }

        public string GenerateToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpiryMinutes)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<(string Role, bool IsValid)> ValidateCredentialsAsync(string username, string password)
        {
            var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.Username == username);
            if (staff != null && staff.PasswordHash == password)
                return ("Staff", true);

            var student = await _context.Students.FirstOrDefaultAsync(s => s.Username == username);
            if (student != null && student.Password == password)
                return ("Student", true);

            return (string.Empty, false);
        }
    }
}
