using MediatR;

namespace DevFreela.Application.Commands.ProjectDetailsViewModel;

public class ProjectDetailsViewModelCommand : IRequest<ViewModels.ProjectDetailsViewModel>
{
    public ProjectDetailsViewModelCommand(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}