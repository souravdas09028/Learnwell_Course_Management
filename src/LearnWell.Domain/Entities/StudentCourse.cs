using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class StudentCourse : BaseEntity
    {
        public StudentCourse() : base() { }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }

}
