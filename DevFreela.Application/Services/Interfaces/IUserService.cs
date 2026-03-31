
using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOS.Input.Users;
using DevFreela.Core.DTOS.Output.Users;

namespace DevFreela.Application.Services.Interfaces;

public interface IUserService
{
    int Create(CreateUserInputModel inputModel);
    int Login(UserViewLoginModel inputModel);
    Task<UserViewModel> GetByIdAsync(int id);
    Task <int> CreateAsync(CreateUserDto createUser);
    Task<LoginUserViewModel?> LoginAsync(LoginUser inputModel);
    Task<List<UserViewModel>> GetAllAsync(GetUsersInput query);
    Task DeleteAsync(DeleteUserInput deleteInput);
    Task CreateUserSkillAsync(int idUser, UserSkillInput inputModel);
}