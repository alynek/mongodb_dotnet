using MongoDB.Driver;
using MongoDotNet.API.Data.Schemas;
using MongoDotNet.API.Domain.Entities;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Data.Repositories
{
    public class RestauranteRepository : IRestauranteRepository
    {
        IMongoCollection<RestauranteSchema> _restaurantes;

        public RestauranteRepository(MongoDB mongoDB)
        {
            _restaurantes = mongoDB.MongoDatabase.GetCollection<RestauranteSchema>("restaurantes");
        }

        public void Inserir(Restaurante restaurante)
        {
            var novoRestaurante = new RestauranteSchema(restaurante.Nome, restaurante.TipoComida,
                new EnderecoSchema(restaurante.Endereco.Logradouro, restaurante.Endereco.Numero,
                    restaurante.Endereco.Cidade, restaurante.Endereco.UF, restaurante.Endereco.Cep)
            );

            _restaurantes.InsertOne(novoRestaurante);
        }

        public async Task<IEnumerable<Restaurante>> ObterTodos()
        {
            var restaurantes = new HashSet<Restaurante>();

            await _restaurantes.AsQueryable().ForEachAsync(schema =>
            {
                var restaurante = new Restaurante(schema.Id, schema.Nome, schema.TipoDeComida);
                var endereco = new Endereco(schema.Endereco.Logradouro, schema.Endereco.Numero,
                    schema.Endereco.Cidade, schema.Endereco.UF, schema.Endereco.Cep);

                restaurante.AtribuirEndereco(endereco);
                restaurantes.Add(restaurante);
            });

            return restaurantes;
        }

        public Restaurante ObterPorId(string id)
        {
            var restaurante = _restaurantes.AsQueryable().FirstOrDefault(_ => _.Id == id);

            return restaurante?.ConverterParaDomain() ?? null;
        }

        public bool AlterarRestaurante(Restaurante restaurante)
        {
            var novoRestaurante = new RestauranteSchema(restaurante.Id, restaurante.Nome, restaurante.TipoComida,
                new EnderecoSchema(restaurante.Endereco.Logradouro, restaurante.Endereco.Numero,
                    restaurante.Endereco.Cidade, restaurante.Endereco.UF, restaurante.Endereco.Cep));

            var resultado = _restaurantes.ReplaceOne(_ => _.Id == novoRestaurante.Id, novoRestaurante);
            return resultado.ModifiedCount > 0;
        }
    }
}
