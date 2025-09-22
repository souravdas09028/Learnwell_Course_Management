using System.ComponentModel.DataAnnotations;

namespace LearnWell.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public DateTime CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }

        protected BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
        protected BaseEntity(Guid createdBy)
        {
            CreatedDate = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void UpdateAuditFields(Guid? modifiedBy)
        {
            ModifiedDate = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }
    }
}
