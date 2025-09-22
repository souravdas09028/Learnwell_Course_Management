using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Implementation;
using LearnWell.Application.Services.Interface;
using LearnWell.Infrastructure.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearnWell.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public void GenerateToken_ReturnsValidJwtToken()
        {
            // Arrange
            var jwtSettings = new JwtSettings
            {
                Key = "ThisIsASecureKeyWi15@th32Chars!!", // must be long enough for HmacSha256
                Issuer = "LearnWell",
                Audience = "LearnWellUsers",
                ExpiryMinutes = 60
            };

            var optionsMock = new Mock<IOptions<JwtSettings>>();
            optionsMock.Setup(o => o.Value).Returns(jwtSettings);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var loggerMock = new Mock<ILogger<UserService>>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            var authService = new AuthService(
                optionsMock.Object,
                unitOfWorkMock.Object,
                loggerMock.Object,
                passwordHasherMock.Object
            );

            var userId = Guid.NewGuid();
            var username = "sourav";
            var role = "Staff";

            // Act
            var tokenResponse = authService.GenerateToken(userId, username, role);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(tokenResponse.Token));
            Assert.True(tokenResponse.Expiration > DateTime.UtcNow);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(tokenResponse.Token);

            Assert.Equal(userId.ToString(), jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            Assert.Equal(username, jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value);
            Assert.Equal(role, jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value);
        }
    }
}
