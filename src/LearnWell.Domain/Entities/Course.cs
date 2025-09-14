using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<Class> _classes = new();
        public IReadOnlyCollection<Class> Classes => _classes.AsReadOnly();

        private readonly List<Student> _students = new();
        public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

        private Course() { } // EF Core needs it

        public Course(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddClass(Class @class)
        {
            if (!_classes.Contains(@class))
                _classes.Add(@class);
        }

        public void EnrollStudent(Student student)
        {
            if (!_students.Contains(student))
                _students.Add(student);
        }    }
}
