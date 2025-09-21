using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Application.Common.DTOs
{
    public record LoginRequest(string Username, string Password);
}
