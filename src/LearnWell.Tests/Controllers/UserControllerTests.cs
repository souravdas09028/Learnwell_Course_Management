using LearnWell.Api.Controllers;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LearnWell.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task RegisterStaff_ReturnsOk()
        {
            // Arrange
            var dto = new RegisterStaffDto
            (
                 "Admin", "Admin", "admin@learnwell.com", "adminpass"
            );

            _userServiceMock
                .Setup(s => s.RegisterStaffAsync(dto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RegisterStaff(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Staff registered", okResult.Value);
        }
    }
}
