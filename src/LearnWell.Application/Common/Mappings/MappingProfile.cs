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
        }
    }
}
