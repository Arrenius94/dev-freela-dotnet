using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _dbContext;
    
    public ProjectService(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public List<ProjectViewModel> GetAll(string query)
    {
        var projects = _dbContext.Projects;

        var filteredProjectsByTitle = string.IsNullOrWhiteSpace(query)
            ? projects
            : projects.Where(p => p.Title.StartsWith(query, StringComparison.OrdinalIgnoreCase)).ToList();
        
        var projectsViewModels = filteredProjectsByTitle
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
            .ToList();
        
        return  projectsViewModels;
    }

    public ProjectDetailsViewModel GetById(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        if (project == null) return null;
            
        var projectDetailsViewModel = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishAt
            );
        return projectDetailsViewModel;
    }

    public int Create(NewProjectInputModel inputModel)
    {
        var project = new 
            Project(inputModel.Title, inputModel.Description, inputModel.IdCliente, inputModel.IdFreelancer, inputModel.TotalCost);
            
        _dbContext.Projects.Add(project);
        
        return project.Id;
    }

    public void Update(UpdateProjectInputModel inputModel)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
        project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
    }

    public void Delete(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project.Cancel();
    }

    public void CreateComment(CreateCommentInputModel inputModel)
    {
        var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
        
        _dbContext.Comments.Add(comment);
    }

    public void Start(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project.Start();    
    }

    public void Finish(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project.Finish();
    }
}