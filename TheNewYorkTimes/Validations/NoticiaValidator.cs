using FluentValidation;
using TheNewYorkTimes.Models;

namespace TheNewYorkTimes.Validations
{
    public class NoticiaValidator : AbstractValidator<Noticia>
    {
        public NoticiaValidator()
        {
            RuleFor(n => n.Titulo)
                .NotEmpty().WithMessage(ErrorMessages.TituloObrigatorio)
                .MaximumLength(50).WithMessage(ErrorMessages.TituloTamanhoMaximo);

            RuleFor(n => n.Descricao)
                .NotEmpty().WithMessage(ErrorMessages.DescricaoObrigatoria)
                .MaximumLength(500).WithMessage(ErrorMessages.DescricaoTamanhoMaximo);

            RuleFor(n => n.Chapeu)
                .NotEmpty().WithMessage(ErrorMessages.ChapeuObrigatoria)
                .MaximumLength(20).WithMessage(ErrorMessages.ChapeuTamanhoMaximo);

            RuleFor(n => n.DataPublicacao)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage(ErrorMessages.DataPublicacaoMenorDataAtual)
                .Must(date => date != default(DateTime)).WithMessage(ErrorMessages.DataPublicacaoObrigatoria);

            RuleFor(n => n.Autor)
                .NotEmpty().WithMessage(ErrorMessages.AutorObrigatorio)
                .MaximumLength(50).WithMessage(ErrorMessages.AutorTamanhoMaximo);
        }
    }
}
