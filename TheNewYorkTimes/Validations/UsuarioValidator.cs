using FluentValidation;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Validations
{
    public class UsuarioValidator : BaseValidator<Usuario>
    {
        public UsuarioValidator()
        {
            Include(new LoginValidator());

            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage(ErrorMessages.NomeObrigatorio)
                .MaximumLength(50).WithMessage(ErrorMessages.NomeTamanhoMaximo);

            RuleFor(u => u.ConfirmeSenha)
                .NotEmpty().WithMessage(ErrorMessages.ConfirmeSenhaObrigatoria)
                .MaximumLength(50).WithMessage(ErrorMessages.ConfirmeSenhaTamanhoMaximo)
                .Equal(u => u.Senha).WithMessage(ErrorMessages.ConfirmeSenhaNaoConfere);
        }
    }
}
