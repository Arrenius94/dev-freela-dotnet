using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOS.Input.SkillsInputDto;
using DevFreela.Core.DTOS.Output.SkillsOutputDto;

namespace DevFreela.Application.Services.Interfaces;

public interface ISkillService
{
    Task<List<SkillDTO>> GetAll(string? query);
    Task<int>Create(SkillCreateDTO skillDTO);
}