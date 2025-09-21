using LearnWell.Api.Controllers;
using LearnWell.Application.Common.DTOs;
using LearnWell.Application.Common.Interfaces;
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
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CourseController _controller;

        public CourseControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            //_controller = new CourseController(_unitOfWorkMock.Object);
        }

        //[Fact]
        //public async Task CreateCourse_ReturnsOkWithCourse()
        //{
        //    var dto = new CreateCourseDto("Math", "Basic Math Course");

        //    var result = await _controller.CreateCourse(dto) as OkObjectResult;

        //    _unitOfWorkMock.Verify(x => x.Courses.AddAsync(It.IsAny<Course>()), Times.Once);
        //    _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        //    Assert.NotNull(result);
        //    Assert.IsType<Course>(result.Value);
        //}

        //[Fact]
        //public async Task GetCourse_ExistingId_ReturnsCourse()
        //{
        //    var courseId = Guid.NewGuid();
        //    var course = new Course { Id = courseId, Name = "Physics" };
        //    _unitOfWorkMock.Setup(x => x.Courses.GetByIdAsync(courseId)).ReturnsAsync(course);

        //    var result = await _controller.GetCourse(courseId) as OkObjectResult;

        //    Assert.NotNull(result);
        //    Assert.Equal(course, result.Value);
        //}

        //[Fact]
        //public async Task GetCourse_NonExistingId_ReturnsNotFound()
        //{
        //    _unitOfWorkMock.Setup(x => x.Courses.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Course?)null);

        //    var result = await _controller.GetCourse(Guid.NewGuid());

        //    Assert.IsType<NotFoundResult>(result);
        //}
    }
}
