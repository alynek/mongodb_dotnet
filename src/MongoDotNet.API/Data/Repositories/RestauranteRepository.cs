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
            var novoRestaurante = new RestauranteSchema
            {
                Nome = restaurante.Nome,
                TipoDeComida = restaurante.TipoComida,
                Endereco = new EnderecoSchema
                {
                    Logradouro = restaurante.Endereco.Logradouro,
                    Numero = restaurante.Endereco.Numero,
                    Cidade = restaurante.Endereco.Cidade,
                    Cep = restaurante.Endereco.Cep,
                    UF = restaurante.Endereco.UF
                }
            };

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
    }
}
