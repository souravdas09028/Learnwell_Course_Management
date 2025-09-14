using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
