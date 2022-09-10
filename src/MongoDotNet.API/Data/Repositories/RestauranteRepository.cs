using MongoDB.Driver;
using MongoDotNet.API.Data.Schemas;
using MongoDotNet.API.Domain.Entities;

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
                TipoDeComida = restaurante.Cozinha,
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
    }
}
