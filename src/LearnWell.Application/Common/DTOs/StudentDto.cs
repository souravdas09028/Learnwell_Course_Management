namespace LearnWell.Application.Common.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        // ✅ Add this
        public StudentDto() { }
    }

}
