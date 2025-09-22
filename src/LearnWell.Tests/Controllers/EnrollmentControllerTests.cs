using LearnWell.Api.Controllers;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Tests.Controllers
{
    public class EnrollmentControllerTests
    {
        private readonly Mock<IEnrollmentService> _enrollmentServiceMock;
        private readonly EnrollmentController _controller;

        public EnrollmentControllerTests()
        {
            _enrollmentServiceMock = new Mock<IEnrollmentService>();
            _controller = new EnrollmentController(_enrollmentServiceMock.Object);

            // Mock authenticated user with "id" claim
            var userId = Guid.NewGuid();
            var claims = new List<Claim> { new Claim("id", userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        [Fact]
        public async Task EnrollInCourse_ReturnsOk_WhenEnrollmentSucceeds()
        {
            // Arrange
            var dto = new EnrollCourseDto
            {
                StudentId = Guid.NewGuid(),
                CourseId = Guid.NewGuid()
            };

            _enrollmentServiceMock
                .Setup(s => s.EnrollStudentInCourseAsync(dto.StudentId, dto.CourseId, It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.EnrollInCourse(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Student enrolled in course and its classes", okResult.Value);
        }
    }
}
