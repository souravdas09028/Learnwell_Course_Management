using LearnWell.Application.Services.Implementation;
using LearnWell.Domain.Entities;
using LearnWell.Infrastructure.Data;
using LearnWell.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Tests.Services
{
    public class EnrollmentServiceTests
    {
        private LearnWellDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LearnWellDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new LearnWellDbContext(options);
        }

        //[Fact]
        //public async Task EnrollStudentInCourse_ShouldEnrollInAllClasses()
        //{
        //    // Arrange
        //    var context = GetDbContext();
        //    var unitOfWork = new UnitOfWork(context);
        //    var service = new EnrollmentService(unitOfWork);

        //    var course = new Course { Id = Guid.NewGuid(), Name = "Math" };
        //    var class1 = new Class { Id = Guid.NewGuid(), Name = "Math A" };
        //    var class2 = new Class { Id = Guid.NewGuid(), Name = "Math B" };
        //    var student = new Student { Id = Guid.NewGuid(), Username = "student1", Password = "pass" };
        //    var staffId = Guid.NewGuid();

        //    await context.Courses.AddAsync(course);
        //    await context.Classes.AddRangeAsync(class1, class2);
        //    await context.Students.AddAsync(student);
        //    await context.CourseClasses.AddRangeAsync(
        //        new CourseClass { CourseId = course.Id, ClassId = class1.Id },
        //        new CourseClass { CourseId = course.Id, ClassId = class2.Id }
        //    );
        //    await context.SaveChangesAsync();

        //    // Act
        //    await service.EnrollStudentInCourseAsync(student.Id, course.Id, staffId);

        //    // Assert
        //    var enrolledClasses = await context.StudentClasses
        //        .Where(sc => sc.StudentId == student.Id)
        //        .ToListAsync();

        //    Assert.Equal(2, enrolledClasses.Count);
        //}
    }
}
