
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.DTOS.Input.SkillsInputDto;
using DevFreela.Core.DTOS.Output.SkillsOutputDto;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
namespace DevFreela.Application.Services.Implementations;

public class SkillService : ISkillService
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly ISkillRepository _skillRepository;
    public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration, ISkillRepository skillRepository)
    {
        _dbContext = dbContext;
        _skillRepository = skillRepository;
    }
    public async Task<List<SkillDTO>> GetAll(string query)
    {
        
        return await _skillRepository.GetAll(query);
        
        /*var skill = _dbContext.Skills;
        var skillModel = skill.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
        return skillModel;*/
    }

    public async Task<int> Create(SkillCreateDTO skillDTO)
    {
        var skill = new Skill (skillDTO.Description);
        
        if (skillDTO.Description.Length < 3)
        {
            throw new ArgumentException("A descrição deve conter pelo menos 3 caracteres.");
        }
        
        var id = await _skillRepository.SaveAsync(skill);
        return id;
    }
}