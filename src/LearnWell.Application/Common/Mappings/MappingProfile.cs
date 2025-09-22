using AutoMapper;
using LearnWell.Application.Common.DTOs;
using LearnWell.Domain.Entities;

namespace LearnWell.Application.Common.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterStudentDto, Student>();
            CreateMap<RegisterStaffDto, Staff>();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<CreateClassDto, Class>();
            CreateMap<ClassDto, Class>().ReverseMap();
            CreateMap<Student, StudentDto>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<StudentDto, StudentCourse>().ReverseMap();
            CreateMap<StudentDto, StudentClass>().ReverseMap();
        }
    }
}
