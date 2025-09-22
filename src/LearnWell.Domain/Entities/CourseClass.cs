using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class CourseClass : BaseEntity
    {
        public CourseClass() : base() { } // For EF Core
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }
    }
}
