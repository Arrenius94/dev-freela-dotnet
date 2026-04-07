using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOS.Input.Users;
using DevFreela.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DevFreela.API.Controllers;
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        catch (DomainException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? query)
    {
        var input = new GetUsersInput { Query = query };

        var users = await _userService.GetAllAsync(input);
        return Ok(users);
    }

    // api/users
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDto createInputUserModel)
    {
            
            var id = await _userService.CreateAsync(createInputUserModel);
            
            return CreatedAtAction(nameof(GetById), new { id = id }, createInputUserModel);
    }

    // api/users//login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUser inputModel)
    {
       
        var loginResult = await _userService.LoginAsync(inputModel);

        if (loginResult == null)
        {
            return Unauthorized(new { message = "Email ou senha inválidos." });
        }
        return Ok(loginResult);
    }

    [HttpDelete("delete{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var deleteInput = new DeleteUserInput { Id = id };
            await _userService.DeleteAsync(deleteInput);
            return Ok(new { message = "Usuário deletado com sucesso!" });

        }
        catch (DomainException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/skills")]
    public async Task<IActionResult> PostSkill(int id, [FromBody] UserSkillInput inputModel)
    {
        await _userService.CreateUserSkillAsync(id, inputModel);
        return NoContent();
    }

}