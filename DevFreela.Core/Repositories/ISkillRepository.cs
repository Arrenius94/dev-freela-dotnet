using DevFreela.Core.DTOS.Output.Projects;
using DevFreela.Core.DTOS.Output.SkillsOutputDto;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface ISkillRepository
{
   Task<List<SkillDTO>> GetAll(string query);
   Task<int> SaveAsync (Skill skill);
}