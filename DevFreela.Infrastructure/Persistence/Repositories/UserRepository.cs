using DevFreela.Core.DTOS.Output.Users;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task AddUserSkillAsync(UserSkill userSkill)
        {
           await _dbContext.UserSkills.AddAsync(userSkill);
           await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsyncRepository(int id)
        {
            var userToDelete = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (userToDelete != null) 
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<List<UserViewModel>> GetAllAsync(string query)
        {
            var usersQuery = _dbContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                usersQuery = usersQuery.Where(u => u.FullName.Contains(query) || u.Email.Contains(query));
            }

            return await usersQuery
                .Select(u => new UserViewModel(u.Id, u.FullName, u.BirthDate))
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

           return user;
        }

        public async Task<int> SaveAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
       
            return user.Id;
        }
       
        public async Task<User?> UserCredentialAsync(string email, string password)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
