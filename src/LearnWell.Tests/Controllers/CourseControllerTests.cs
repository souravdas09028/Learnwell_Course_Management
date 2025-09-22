using LearnWell.Api.Controllers;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
using LearnWell.Application.Services.Interface;
using LearnWell.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Tests.Controllers
{
    public class CourseControllerTests
    {
        private readonly Mock<ICourseService> _courseServiceMock;
        private readonly CourseController _controller;

        public CourseControllerTests()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _controller = new CourseController(_courseServiceMock.Object);
        }

        [Fact]
        public async Task CreateCourse_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockService = new Mock<ICourseService>();
            var controller = new CourseController(mockService.Object);
            var dto = new CreateCourseDto("Programming", "TestDesc");

            // Act
            var result = await controller.CreateCourse(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(controller.GetCourse), createdResult.ActionName);
            Assert.Equal(dto, createdResult.Value);
        }

        [Fact]
        public async Task GetCourse_WithValidId_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<ICourseService>();
            var controller = new CourseController(mockService.Object);
            var courseId = Guid.NewGuid();
            var courseDto = new CourseDto(courseId, "Marketing");

            mockService.Setup(s => s.GetAsync(courseId)).ReturnsAsync(courseDto);

            // Act
            var result = await controller.GetCourse(courseId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(courseDto, okResult.Value);
        }

        [Fact]
        public async Task DeleteCourse_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<ICourseService>();
            var controller = new CourseController(mockService.Object);
            var courseId = Guid.NewGuid();

            mockService.Setup(s => s.GetAsync(courseId)).ReturnsAsync((CourseDto)null);

            // Act
            var result = await controller.DeleteCourse(courseId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllCourses_ReturnsOkWithCourses()
        {
            // Arrange
            var courseList = new List<CourseDto>
            {
                new CourseDto ( Guid.NewGuid(),  "Programming" ),
                new CourseDto (Guid.NewGuid(),  "Marketing" )
            };

            _courseServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(courseList);

            // Act
            var result = await _controller.GetAllCourses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCourses = Assert.IsAssignableFrom<IEnumerable<CourseDto>>(okResult.Value);

            Assert.Equal(2, returnedCourses.Count());
            Assert.Contains(returnedCourses, c => c.Name == "Programming");
            Assert.Contains(returnedCourses, c => c.Name == "Marketing");
        }
    }
}
