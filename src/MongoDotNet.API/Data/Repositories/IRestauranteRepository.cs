using MongoDotNet.API.Domain.Entities;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Data.Repositories
{
    public interface IRestauranteRepository
    {
        public void Inserir(Restaurante restaurante);

        public Task<IEnumerable<Restaurante>> ObterTodos();

        public Restaurante ObterPorId(string id);

        public bool AlterarRestaurante(Restaurante restaurante);

        public IEnumerable<Restaurante> ObterPorNome(string nome);

        public void Avaliar(string restauranteId, Avaliacao avaliacao);

        public Dictionary<Restaurante, double> ObterTop3();

        public (long, long) Remover(string restauranteId);
    }
}