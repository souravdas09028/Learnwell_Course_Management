using LearnWell.Api.Controllers;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var tokenResponse = new JwtTokenResponse
            {
                Token = "mocked-token",
                Expiration = DateTime.UtcNow.AddMinutes(30)
            };

            _authServiceMock.Setup(x => x.ValidateCredentialsAsync("staff", "mypassword"))
                .ReturnsAsync(("Staff", true));
            _authServiceMock.Setup(x => x.GenerateToken("staff", "Staff")) // assuming GenerateToken takes userId, username, role
                .Returns(tokenResponse);

            // Act
            var result = await _controller.Login(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var value = Assert.IsType<JwtTokenResponse>(result.Value);
            Assert.Equal("mocked-token", value.Token);
        }


        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var request = new LoginRequest("unknown", "wrong");
            _authServiceMock.Setup(x => x.ValidateCredentialsAsync("unknown", "wrong"))
                .ReturnsAsync((string.Empty, false));

            var result = await _controller.Login(request);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }

    }
}
