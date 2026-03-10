using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.ProjectDetailsViewModel;
using DevFreela.Application.Commands.ProjectViewModel;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMediator _mediator;
    public ProjectsController(IProjectService projectService, IMediator mediator)
    {
       _projectService = projectService;
       _mediator = mediator;
    }
    // api/projects?query=net core
    [HttpGet]
    public async Task<IActionResult> Get(string query)
    {
        /*var projects = _projectService.GetAll(query);*/
        var command = new ProjectViewModelCommand(query);
        var projects = await _mediator.Send(command);

        if (projects == null)
        {
            return NotFound();
        }
        return Ok(projects); 
    }

    // api/projects/599
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById (int id)
    {
        /*var project = _projectService.GetById(id);*/
        var query = new ProjectDetailsViewModelCommand(id);
        var project = await _mediator.Send(query);
        if (project == null)
        {
            return NotFound();
        }
        // Buscar o projeto por ID
        return Ok(project);
       
    }

    [HttpPost]
    public async Task<IActionResult> Post ([FromBody] CreateProjectCommand comand)
    {
        if (comand.Title.Length > 50)
        {
            return BadRequest("Nao pode ser maior q 50 caracteres");
        }
        // cadastrar o projeto
        /*var id = _projectService.Create(inputModel);*/
        var id = await _mediator.Send(comand);
        return CreatedAtAction(nameof(GetById), new {id = id}, comand);
    }
    
    // api/projects/2
    [HttpPut("{id}")]
    public async Task<IActionResult> Put (int id, [FromBody] UpdateProjectCommand command)
    {
        command.Id = id;
        if (command.Description.Length > 200)
        {
            return BadRequest();
        }
        await  _mediator.Send(command);
        /*_projectService.Update(inputProject);*/
        // Att o objeto
        return NoContent();
    }
    
    // api/projects/3
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        /*_projectService.Delete(id);*/
        var command = new DeleteProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    // api/projects/1/comments
    [HttpPost("{id}/coments")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateProjectCommand comand)
    {
       await _mediator.Send(comand);
       
        return NoContent();
    }
    
    // api/projects/1/start
    [HttpPut("{id}/start")]
    public async Task<IActionResult> Start(int id)
    {
        /*_projectService.Start(id);*/
        var command = new StartProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    // api/projects/1/finish
    [HttpPut("{id}/finish")]
    public async Task<IActionResult> Finish(int id)
    {
        /*_projectService.Finish(id);*/
        var command = new FinishProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}