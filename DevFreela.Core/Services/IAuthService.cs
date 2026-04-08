using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, EUserRole role);
        string ComputeSha256Hash(string password);
    }
}
