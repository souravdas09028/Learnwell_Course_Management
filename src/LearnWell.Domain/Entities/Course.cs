using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        private readonly List<CourseClass> _classes = new();
        public IReadOnlyCollection<CourseClass> Classes => _classes.AsReadOnly();

        private readonly List<StudentCourse> _students = new();
        public IReadOnlyCollection<StudentCourse> Students => _students.AsReadOnly();

        private Course() : base() { }

        public Course(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddClass(CourseClass @class)
        {
            if (!_classes.Contains(@class))
                _classes.Add(@class);
        }

        public void EnrollStudent(StudentCourse student)
        {
            if (!_students.Contains(student))
                _students.Add(student);
        }
    }
}
