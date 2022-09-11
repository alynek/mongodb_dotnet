using MongoDotNet.API.Domain.Entities;

namespace MongoDotNet.API.Data.Repositories
{
    public interface IRestauranteRepository
    {
        public void Inserir(Restaurante restaurante);

        public Task<IEnumerable<Restaurante>> ObterTodos();

        public Restaurante ObterPorId(string id);
    }
}