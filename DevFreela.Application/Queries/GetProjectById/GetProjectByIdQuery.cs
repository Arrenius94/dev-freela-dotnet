using Azure.Core;
using DevFreela.Application.ViewModels;
using DevFreela.Core.DTOS.Output.Projects;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ProjectDetailsDTO>
{
    public GetProjectByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}