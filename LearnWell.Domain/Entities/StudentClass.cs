namespace LearnWell.Domain.Entities
{
    public class StudentClass
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public Guid AssignedByStaffId { get; set; }
        public Staff AssignedBy { get; set; }

        public DateTime AssignedAt { get; set; }
    }
}
