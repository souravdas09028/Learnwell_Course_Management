using LearnWell.Application.Common.DTOs;
namespace LearnWell.Application.Common.Interfaces
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(string username, string role);
        Task<(string Role, bool IsValid)> ValidateCredentialsAsync(string username, string password);
    }
}
