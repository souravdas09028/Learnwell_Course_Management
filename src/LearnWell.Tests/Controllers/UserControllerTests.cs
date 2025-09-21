using LearnWell.Api.Controllers;
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
    public class UserControllerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            //_controller = new UserController(_unitOfWorkMock.Object);
        }

        //[Fact]
        //public async Task RegisterStudent_ReturnsOk()
        //{
        //    var dto = new RegisterStudentDto("student1", "pass", "Student One", "student@example.com");

        //    var result = await _controller.RegisterStudent(dto);

        //    _unitOfWorkMock.Verify(x => x.Students.AddAsync(It.IsAny<Student>()), Times.Once);
        //    _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public async Task RegisterStaff_ReturnsOk()
        //{
        //    var dto = new RegisterStaffDto("staff1", "pass", "Staff One", "staff@example.com");

        //    var result = await _controller.RegisterStaff(dto);

        //    _unitOfWorkMock.Verify(x => x.Staffs.AddAsync(It.IsAny<Staff>()), Times.Once);
        //    _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        //    Assert.IsType<OkObjectResult>(result);
        //}
    }
}
