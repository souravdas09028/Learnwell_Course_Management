using LearnWell.Api.Controllers;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace LearnWell.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var request = new LoginRequest("staff", "mypassword");
            var expectedToken = new JwtTokenResponse
            {
                Token = "mocked-jwt-token",
                Expiration = DateTime.UtcNow.AddHours(1)
            };

            _authServiceMock
                .Setup(s => s.ValidateCredentialsAsync(request.Username, request.Password))
                .ReturnsAsync((Guid.NewGuid(), "Staff", true));

            _authServiceMock
                .Setup(s => s.GenerateToken(It.IsAny<Guid>(), request.Username, "Staff"))
                .Returns(expectedToken);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<AuthResponseDto>(okResult.Value);

            Assert.Equal(expectedToken.Token, response.Token);
            Assert.Equal(expectedToken.Expiration, response.Expiration);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest("wronguser", "wrongpass");

            _authServiceMock
                .Setup(s => s.ValidateCredentialsAsync(request.Username, request.Password))
                .ReturnsAsync((Guid.Empty, "", false));

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid username or password", unauthorizedResult.Value);
        }
    }
}
