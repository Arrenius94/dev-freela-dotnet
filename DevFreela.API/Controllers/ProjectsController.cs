using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectsController(IProjectService projectService)
    {
       _projectService = projectService;
       
    }
    // api/projects?query=net core
    [HttpGet]
    public IActionResult Get(string query)
    {
        var projects = _projectService.GetAll(query);
        return Ok(projects); 
    }

    // api/projects/599
    [HttpGet("{id}")]
    public IActionResult GetById (int id)
    {
        var project = _projectService.GetById(id);
        if (project == null)
        {
            // return not found();
            return NotFound();
        }
        // Buscar o projeto por ID
        return Ok(project);
       
    }

    [HttpPost]
    public IActionResult Post ([FromBody] NewProjectInputModel inputModel)
    {
        if (inputModel.Title.Length > 50)
        {
            return BadRequest("Nao pode ser maior q 50 caracteres");
        }
        // cadastrar o projeto
        var id = _projectService.Create(inputModel);
        return CreatedAtAction(nameof(GetById), new {id = id}, inputModel);
    }
    
    // api/projects/2
    [HttpPut("{id}")]
    public IActionResult Put (int id, [FromBody] UpdateProjectInputModel inputProject)
    {
        if (inputProject.Description.Length > 200)
        {
            return BadRequest();
        }
        
        _projectService.Update(inputProject);
        // Att o objeto
        return NoContent();
    }
    
    // api/projects/3
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _projectService.Delete(id);
        
        // Buscar, se nao existir retorna not foud
        
        // Remover
        return NoContent();
    }
    
    // api/projects/1/comments
    [HttpPost("{id}/coments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
    {
        _projectService.CreateComment(inputModel);
        return NoContent();
    }
    
    // api/projects/1/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        _projectService.Start(id);
        return NoContent();
    }

    // api/projects/1/finish
    [HttpPut("{id}/finish")]
    public IActionResult Finish(int id)
    {
        _projectService.Finish(id);
        return NoContent();
    }
}