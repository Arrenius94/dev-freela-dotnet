using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectViewModel;

public class ProjectViewModelCommandHandler : IRequestHandler<ProjectViewModelCommand, List<ViewModels.ProjectViewModel>>
{
    private readonly DevFreelaDbContext _dbContext;
    
    public ProjectViewModelCommandHandler(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<ViewModels.ProjectViewModel>> Handle(ProjectViewModelCommand request, CancellationToken cancellationToken)
    {
        var projects = _dbContext.Projects;

        var filteredProjects = string.IsNullOrWhiteSpace(request.Query)
            ? projects
            : projects.Where(p => p.Title.Contains(request.Query));

        var projectViewModel = await filteredProjects
            .AsNoTracking()
            .Select(p => new ViewModels.ProjectViewModel(p.Id, p.Title, p.CreatedAt))
            .ToListAsync(cancellationToken);
        
        return projectViewModel;
    }
}