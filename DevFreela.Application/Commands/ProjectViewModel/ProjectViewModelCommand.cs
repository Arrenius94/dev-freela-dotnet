using MediatR;

namespace DevFreela.Application.Commands.ProjectViewModel;

public class ProjectViewModelCommand : IRequest<List<ViewModels.ProjectViewModel>>
{
    public ProjectViewModelCommand(string query)
    {
        Query = query;
    }
    public string Query { get; set; }
}