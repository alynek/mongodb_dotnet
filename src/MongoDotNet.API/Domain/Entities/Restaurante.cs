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

        public Restaurante(string id, string nome, ECozinha cozinha)
        {
            Id = id;
            Nome = nome;
            Cozinha = cozinha;
        }
    }
}
