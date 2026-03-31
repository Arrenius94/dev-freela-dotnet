using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOS.Output.Projects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsDTO?>
{
    private readonly IProjectRepository projectRepository;
    public GetProjectByIdQueryHandler(IProjectRepository _projectRepository)
    {
        projectRepository = _projectRepository;
    }

    public async Task<ProjectDetailsDTO?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0) return null;

        var projectDetails = await projectRepository.GetById(request.Id);

        return projectDetails;
    }
}