using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTOS.Output.Users
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel(string fullName, string token)
        {
            FullName = fullName;
            Token = token;
        }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
