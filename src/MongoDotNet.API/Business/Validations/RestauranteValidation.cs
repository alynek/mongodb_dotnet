using FluentValidation;
using MongoDotNet.API.Domain.Entities;

namespace MongoDotNet.API.Business.Validations
{
    public class RestauranteValidation : AbstractValidator<Restaurante>
    {
        public RestauranteValidation()
        {
            RuleFor(r => r.Nome)
                .NotEmpty().WithMessage("Nome não pode ser vazio.")
                .MaximumLength(30).WithMessage("Nome pode ter no máximo 30 caracteres");
        }
    }
}
