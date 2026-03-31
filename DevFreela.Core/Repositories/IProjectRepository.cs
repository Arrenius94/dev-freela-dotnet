using DevFreela.Core.DTOS.Output.Projects;
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    Task<int> SaveAsync(Project project);
    Task<List<ProjectListDTO>> GetAll(string query);
    Task<ProjectDetailsDTO?> GetById(int id);
    Task AddCommentAsync(ProjectComment projectComment);
    Task<Project?> CancelAsync(int id);
}
