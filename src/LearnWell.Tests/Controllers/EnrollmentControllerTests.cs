using LearnWell.Api.Controllers;
using LearnWell.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        //[Fact]
        //public async Task EnrollInCourse_ReturnsOk()
        //{
        //    var dto = new EnrollCourseDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        //    var result = await _controller.EnrollInCourse(dto);

        //    _enrollmentServiceMock.Verify(x => x.EnrollStudentInCourseAsync(dto.StudentId, dto.CourseId, dto.StaffId), Times.Once);
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public async Task EnrollInClass_ReturnsOk()
        //{
        //    var dto = new EnrollClassDto(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        //    var result = await _controller.EnrollInClass(dto);

        //    _enrollmentServiceMock.Verify(x => x.EnrollStudentInClassAsync(dto.StudentId, dto.ClassId, dto.StaffId), Times.Once);
        //    Assert.IsType<OkObjectResult>(result);
        //}
    }
}
