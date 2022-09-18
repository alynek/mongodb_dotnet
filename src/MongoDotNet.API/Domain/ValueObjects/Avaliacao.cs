using FluentValidation.Results;
using MongoDotNet.API.Business.Validations;

namespace MongoDotNet.API.Domain.ValueObjects
{
    public class Avaliacao
    {
        public int Estrelas { get; set; }
        public string Comentario { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public Avaliacao() { }
        public Avaliacao(int estrelas, string comentario)
        {
            Estrelas = estrelas;
            Comentario = comentario;
        }

        public bool Validar()
        {
            ValidationResult = new AvaliacaoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
