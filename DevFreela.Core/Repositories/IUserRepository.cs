using DevFreela.Core.DTOS.Output.Users;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository
    {
      Task <int> SaveAsync(User user);
      Task<User?> UserCredentialAsync(string email, string passwordHash);
      Task<User?> GetByIdAsync(int id);
      Task<List<UserViewModel>> GetAllAsync(string query);
      Task DeleteAsyncRepository (int id);
      Task AddUserSkillAsync(UserSkill userSkill);
    }
}
