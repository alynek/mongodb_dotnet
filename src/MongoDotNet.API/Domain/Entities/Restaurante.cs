using FluentValidation.Results;
using MongoDotNet.API.Business.Validations;
using MongoDotNet.API.Domain.Enums;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Domain.Entities
{
    public class Restaurante
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        public ECozinha Cozinha { get; private set; }

        public Endereco Endereco { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        public Restaurante(string id, string nome, ECozinha cozinha)
        {
            Id = id;
            Nome = nome;
            Cozinha = cozinha;
        }

        public void AtribuirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }

        public bool Validar()
        {
            ValidationResult = new RestauranteValidation().Validate(this);
            ValidarEndereco();

            return ValidationResult.IsValid;
        }

        private void ValidarEndereco()
        {
            if (Endereco.Validar()) return;

            Endereco.ValidationResult.Errors.ForEach(erro => ValidationResult.Errors.Add(erro));
        }
    }
}
