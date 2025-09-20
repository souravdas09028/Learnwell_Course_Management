using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWell.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string username, string role);
        Task<(string Role, bool IsValid)> ValidateCredentialsAsync(string username, string password);
    }
}
