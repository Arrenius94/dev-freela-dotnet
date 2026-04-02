using System.Text.RegularExpressions;
using DevFreela.Core.DTOS.Input.Users;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateUserValidation : AbstractValidator<CreateUserDto>
{
    public CreateUserValidation()
    {
        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage("O email deve ser válido.");

        RuleFor(p => p.Password)
            .Must(ValidatePassword)
            .WithMessage("A senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");
        
        RuleFor(p => p.FullName)
            .NotEmpty()
            .NotNull()
            .WithMessage("O nome completo é obrigatório.");
    }

    public bool ValidatePassword(string password)
    {
        var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
        return regex.IsMatch(password);
    }
}