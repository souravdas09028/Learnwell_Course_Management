namespace LearnWell.Domain.Entities
{
    public class CourseClass
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }
    }
}
