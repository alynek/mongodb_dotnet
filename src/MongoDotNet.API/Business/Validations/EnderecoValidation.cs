using FluentValidation;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Business.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            RuleFor(l => l.Logradouro)
                .NotEmpty().WithMessage("Logradouro não pode ser vazio.")
                .MaximumLength(50).WithMessage("Logradouro pode ter no máximo 50 caracteres");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("Cidade não pode ser vazia.")
                .MaximumLength(100).WithMessage("Cidade pode ter no máximo 100 caracteres");

            RuleFor(u => u.UF)
                .NotEmpty().WithMessage("UF não pode ser vazio.")
                .Length(2).WithMessage("UF deve ter 2 caracteres");

            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage("Cep não pode ser vazio.")
                .Length(8).WithMessage("Cep deve ter 8 caracteres");
        }
    }
}
