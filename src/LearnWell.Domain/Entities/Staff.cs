using LearnWell.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnWell.Domain.Entities
{
    [Table("staff")]
    public class Staff : BaseEntity
    {
        public string FullName { get; private set; }
        public string Username { get; private set; }
        public string HashedPassword { get; private set; }

        private Staff() { }

        public Staff(string fullName, string username, string passwordHash)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            HashedPassword = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }
    }
}
