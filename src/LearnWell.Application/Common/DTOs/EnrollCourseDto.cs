namespace LearnWell.Application.Common.DTOs
{
    public class EnrollCourseDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public Guid StaffId { get; set; }
    }
}
