using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.DTOS.Input.SkillsInputDto;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;
[Route("api/skills")]
[ApiController]
public class SkillsController : ControllerBase
{
        private readonly ISkillService _skillService;
        // Injetando o Service pelo construtor
        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        
        [HttpPost]
        public async Task<ActionResult<SkillCreateDTO>> Create(SkillCreateDTO skillDTO)
        {
            try
            {
                var id = await _skillService.Create(skillDTO);
                return CreatedAtAction(nameof(Get), new { id = id }, skillDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Get(string? query)
        {
            
            var skills = await _skillService.GetAll(query);
            return Ok(skills); 
        }
}