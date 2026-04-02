using DevFreela.Core.DTOS.Input.Users;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class LoginUserValidation : AbstractValidator<LoginUser>
{
    public LoginUserValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O email é obrigatório.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("A senha é obrigatória.");

    }
}