using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
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
    public async Task<IActionResult> Get(string? query)
    {
        /*var projects = _projectService.GetAll(query);*/
        if (!string.IsNullOrWhiteSpace(query) && query.Length < 2)
        {
            return BadRequest("Para filtrar, digite pelo menos 2 caracteres");
        }
        
        var command = new GetAllProjectsQuery(query);
        var projects = await _mediator.Send(command);
        
        return Ok(projects);
    }

    // api/projects/599
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById (int id)
    {
        /*var project = _projectService.GetById(id);*/
        var query = new GetProjectByIdQuery(id);
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
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand comand)
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