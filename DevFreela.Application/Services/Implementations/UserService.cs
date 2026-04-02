using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOS.Input.Users;
using DevFreela.Core.DTOS.Output.Users;
using DevFreela.Core.Entities;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    public UserService(DevFreelaDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }
    
    //public UserViewModel GetById(int id)
    //{
    //    var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);
    //    if (user == null) return null;

    //    var userViewModel = new UserViewModel(
    //        user.FullName,
    //        user.Email,
    //        user.CreatedAt
    //    );
    //    return userViewModel;
    //}

    public int Create(CreateUserInputModel inputModel)
    {
        var user = new User(inputModel.FullName, inputModel.Email, inputModel.Password, inputModel.BirthDate);
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user.Id;
    }

    public int Login(UserViewLoginModel inputModel)
    {
       var user = _dbContext.Users.SingleOrDefault(u => u.FullName == inputModel.Username && u.Password == inputModel.Password);
       if (user == null) return 0;
       return user.Id;
    }

    async Task<int> IUserService.CreateAsync(CreateUserDto createUser)
    {
        var user = new User(createUser.FullName, createUser.Email, createUser.Password, createUser.BirthDate);
        await _userRepository.SaveAsync(user);
        return user.Id;
    }

    public async Task<LoginUserViewModel?> LoginAsync(LoginUser inputModel)
    { 
        var user = await _userRepository.UserCredentialAsync(inputModel.Email, inputModel.Password);
        if (user == null) return null;

        return new LoginUserViewModel(user.Id, user.FullName, "token");
    }

    public async Task<List<UserViewModel>> GetAllAsync(GetUsersInput query)
    {
        var search = query?.Query ?? "";
        return await _userRepository.GetAllAsync(search);
    }

    public async Task<UserViewModel> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
       
        if (user == null)
        {
            throw new DomainException("Esse Id não existe");
        }

        return new UserViewModel(user.Id, user.FullName, user.BirthDate);
    
    }

    public async Task DeleteAsync(DeleteUserInput deleteInput)
    {
        var user = await _userRepository.GetByIdAsync(deleteInput.Id);
        if (user == null)
        {
            throw new DomainException("Esse Id não existe");
        }

        await _userRepository.DeleteAsyncRepository(user.Id);

    }

    public async Task CreateUserSkillAsync(int idUser, UserSkillInput inputModel)
    {
        var userSkill = new UserSkill(idUser, inputModel.IdSkill);

        await _userRepository.AddUserSkillAsync(userSkill);
    }
} 