using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Application.Common.DTOs
{
    public class EnrollClassDto
    {
        public Guid StudentId { get; set; }
        public Guid ClassId { get; set; }
        public Guid StaffId { get; set; }
    }
}
