using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(command => command.Description)
            .MaximumLength(200)
            .WithMessage("A descrição não pode ter mais de 200 caracteres.")
            .NotEmpty()
            .WithMessage("A descrição não pode ser vazia.");
    }
}