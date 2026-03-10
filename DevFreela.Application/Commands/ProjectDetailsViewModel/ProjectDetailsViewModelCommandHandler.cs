using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectDetailsViewModel;

public class ProjectDetailsViewModelCommandHandler : IRequestHandler<ProjectDetailsViewModelCommand, ViewModels.ProjectDetailsViewModel>
{
    private readonly DevFreelaDbContext _dbContext;
    public ProjectDetailsViewModelCommandHandler(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ViewModels.ProjectDetailsViewModel> Handle(ProjectDetailsViewModelCommand request, CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .AsNoTracking()
            .Include(p => p.Client)
            .Include(p => p.FreeLancer)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (project == null) return null;
            
        var projectDetailsViewModel = new ViewModels.ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishAt,
            project.Client.FullName,
            project.FreeLancer.FullName
        );
        return projectDetailsViewModel;
    }
}