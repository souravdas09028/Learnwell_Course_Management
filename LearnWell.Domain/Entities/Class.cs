using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<Student> _students = new();
        public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

        private readonly List<Course> _courses = new();
        public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

        private Class() { }

        public Class(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AssignToCourse(Course course)
        {
            if (!_courses.Contains(course))
                _courses.Add(course);
        }

        public void EnrollStudent(Student student)
        {
            if (!_students.Contains(student))
                _students.Add(student);
        }
    }
}
