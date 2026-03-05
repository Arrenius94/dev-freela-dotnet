using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
        
    }
    
    // api/users
    [HttpPost]
    public IActionResult Post([FromBody] CreateUserInputModel createInputUserModel)
    {
        if (createInputUserModel is null)
        {
            return BadRequest("Os dados do usuário são obrigatórios.");
        }
        var id = _userService.Create(createInputUserModel);
        return CreatedAtAction (nameof(GetById), new {id = id}, createInputUserModel);
    }

    // api/users/1/login
    [HttpPut("{id}/login")]
    public IActionResult Login(int id, [FromBody] UserViewLoginModel loginModel)
    {
        if (loginModel is null)
        {
            return BadRequest("Os dados de login são obrigatórios.");
        }
        var userId = _userService.Login(loginModel);
        if (userId == 0)
        {
            return Unauthorized("Credenciais inválidas.");
        }
        return Ok($"Usuário com ID {userId} autenticado com sucesso.");
    }
}