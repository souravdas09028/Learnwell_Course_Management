using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string FullName { get; private set; }

        private readonly List<StudentClass> _classes = new();
        public IReadOnlyCollection<StudentClass> Classes => _classes.AsReadOnly();

        private readonly List<StudentCourse> _courses = new();
        public IReadOnlyCollection<StudentCourse> Courses => _courses.AsReadOnly();

        private Student() : base() { } // EF Core needs it

        public Student(string fullName, string username, string passwordHash)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            HashedPassword = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }

        public void EnrollInClass(StudentClass @class)
        {
            if (!_classes.Contains(@class))
                _classes.Add(@class);
        }

        public void EnrollInCourse(StudentCourse course)
        {
            if (!_courses.Contains(course))
                _courses.Add(course);
        }
    }
}
