using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(p => p.Description)
            .MaximumLength(255)
            .WithMessage("A descrição deve conter no máximo 255 caracteres.");
        
        RuleFor(p => p.Title)
            .MaximumLength(30)
            .WithMessage("O título deve conter no máximo 30 caracteres.");
    }
}