using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly DevFreelaDbContext _dbContext;
    
    public UserService(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public UserViewModel GetById(int id)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
        if (user == null) return null;

        var userViewModel = new UserViewModel(
            user.FullName,
            user.Email,
            user.CreatedAt
        );
        return userViewModel;
    }

    public int Create(CreateUserInputModel inputModel)
    {
        var user = new User(inputModel.FullName, inputModel.Email, inputModel.Password, inputModel.BirthDate);
        _dbContext.Users.Add(user);
        
        return user.Id;
    }

    public int Login(UserViewLoginModel inputModel)
    {
       var user = _dbContext.Users.SingleOrDefault(u => u.FullName == inputModel.Username && u.Password == inputModel.Password);
       if (user == null) return 0;
       return user.Id;
    }
}