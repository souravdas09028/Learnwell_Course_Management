using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; protected set; }

        private readonly List<StudentClass> _students = new();
        public IReadOnlyCollection<StudentClass> Students => _students.AsReadOnly();

        private readonly List<CourseClass> _courses = new();
        public IReadOnlyCollection<CourseClass> Courses => _courses.AsReadOnly();

        private Class() :base()
        { }

        public Class(string name, Guid createdBy) : base(createdBy)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void UpdateName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AssignToCourse(CourseClass course)
        {
            if (!_courses.Contains(course))
                _courses.Add(course);
        }

        public void EnrollStudent(StudentClass student)
        {
            if (!_students.Contains(student))
                _students.Add(student);
        }
    }
}
