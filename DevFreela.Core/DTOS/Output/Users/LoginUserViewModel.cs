using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTOS.Output.Users
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel(int id, string fullName, string token)
        {

            Id = id;
            FullName = fullName;
            Token = token;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
