using LearnWell.Domain.Entities.Base;

namespace LearnWell.Domain.Entities
{
    public class Staff : BaseEntity
    {
        public string FullName { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }

        private Staff() { } 

        public Staff(string fullName, string username, string passwordHash)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }

    }
}
