using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly OpenTimeOption _option;

    public ProjectsController(IOptions<OpenTimeOption> option, ExampleClass exampleClass)
    {
        exampleClass.Name = "Update at ProjectsController";
        _option = option.Value;
    }
    
    // api/projects?query=net core
    [HttpGet]
    public IActionResult Get(string query)
    {
        // buscar / filtrar todos
        return Ok("Sucess! :)");
    }

    // api/projects/599
    [HttpGet("{id}")]
    public IActionResult GetById (int id)
    {
        // Buscar o projeto por ID
        // return not found();
        return Ok("Sucess! ID encontrado");
    }

    [HttpPost]
    public IActionResult Post ([FromBody] CreateProjectModel createProject)
    {
        if (createProject.Title.Length > 50)
        {
            return BadRequest("Nao pode ser maior q 50 caracteres");
        }
        // cadastrar o projeto
        return CreatedAtAction(nameof(GetById), new {id = createProject.Id}, createProject);
    }
    
    // api/projects/2
    [HttpPut("{id}")]
    public IActionResult Put (int id, [FromBody] UpdateProjectModel updateProject)
    {
        if (updateProject.Description.Length > 50)
        {
            return BadRequest();
        }
        
        // Att o objeto
        return NoContent();
    }
    
    // api/projects/3
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // Buscar, se nao existir retorna not foud
        
        // Remover
        return NoContent();
    }
    
    // api/projects/1/comments
    [HttpPost("{id}/coments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentModel createCommentModel)
    {
        return NoContent();
    }
    
    // api/projects/1/start
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        return NoContent();
    }

    // api/projects/1/finish
    [HttpPut("{id}/finish")]
    public IActionResult Finish(int id)
    {
        return NoContent();
    }
}