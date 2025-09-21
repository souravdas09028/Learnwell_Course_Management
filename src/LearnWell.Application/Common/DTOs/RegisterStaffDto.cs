using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Application.Common.DTOs
{
    public record RegisterStaffDto(
        string Username,
        string Password,
        string FullName,
        string Email
    );
}
