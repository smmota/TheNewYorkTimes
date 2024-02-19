using FluentValidation;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Validations
{
    public class LoginValidator : BaseValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage(ErrorMessages.EmailObrigatorio)
                .MaximumLength(50).WithMessage(ErrorMessages.EmailTamanhoMaximo)
                .EmailAddress().WithMessage(ErrorMessages.EmailFormatoInvalido);

            RuleFor(l => l.Senha)
                .NotEmpty().WithMessage(ErrorMessages.SenhaObrigatoria)
                .MaximumLength(10).WithMessage(ErrorMessages.SenhaTamanhoMaximo);
        }
    }
}
