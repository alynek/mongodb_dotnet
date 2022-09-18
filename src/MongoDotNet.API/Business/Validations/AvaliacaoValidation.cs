using FluentValidation;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Business.Validations
{
    public class AvaliacaoValidation : AbstractValidator<Avaliacao>
    {
        public AvaliacaoValidation()
        {
            RuleFor(a => a.Estrelas)
                .GreaterThan(0).WithMessage("Numero de estrelas deve ser maior que zero")
                .LessThanOrEqualTo(5).WithMessage("Número de estrelas deve ser menor ou igual a cinco");

            RuleFor(a => a.Comentario)
                .NotEmpty().WithMessage("Comentário não pode ser vazio")
                .MaximumLength(100).WithMessage("Comentário pode ter no máximo 100 caracteres");
        }
    }
}
