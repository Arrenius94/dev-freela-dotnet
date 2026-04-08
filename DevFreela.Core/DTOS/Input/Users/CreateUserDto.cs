using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;

namespace DevFreela.Core.DTOS.Input.Users
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EUserRole Role { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
