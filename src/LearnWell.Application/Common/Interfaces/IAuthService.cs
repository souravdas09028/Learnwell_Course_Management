using LearnWell.Application.Common.DTOs;
namespace LearnWell.Application.Common.Interfaces
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(Guid userId, string username, string role);
        Task<(Guid userId, string Role, bool IsValid)> ValidateCredentialsAsync(string username, string password);
    }
}
