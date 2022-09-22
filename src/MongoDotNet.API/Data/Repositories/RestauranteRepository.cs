using MongoDB.Driver;
using MongoDotNet.API.Data.Schemas;
using MongoDotNet.API.Domain.Entities;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Data.Repositories
{
    public class RestauranteRepository : IRestauranteRepository
    {
        IMongoCollection<RestauranteSchema> _restaurantes;
        IMongoCollection<AvaliacaoSchema> _avaliacoes;

        public RestauranteRepository(MongoDB mongoDB)
        {
            _restaurantes = mongoDB.MongoDatabase.GetCollection<RestauranteSchema>("restaurantes");
            _avaliacoes = mongoDB.MongoDatabase.GetCollection<AvaliacaoSchema>("avaliacoes");
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

        public IEnumerable<Restaurante> ObterPorNome(string nome)
        {
            var restaurantes = new HashSet<Restaurante>();

            _restaurantes.AsQueryable()
                .Where(r => r.Nome.ToLower().Contains(nome.ToLower()))
                .ToList()
                .ForEach(r => restaurantes.Add(r.ConverterParaDomain()));

            return restaurantes;
        }

        public void Avaliar(string restauranteId, Avaliacao avaliacao)
        {
            var avaliacaoSchema = new AvaliacaoSchema
            {
                RestauranteId = restauranteId,
                Estrelas = avaliacao.Estrelas,
                Comentario = avaliacao.Comentario
            };

            _avaliacoes.InsertOne(avaliacaoSchema);
        }

        public Dictionary<Restaurante, double> ObterTop3()
        {
            var retorno = new Dictionary<Restaurante, double>();

            var top3Avaliacoes = _avaliacoes.AsQueryable()
                    .GroupBy(r => r.RestauranteId)
                    .Select(_ => new
                        {
                            RestauranteId = _.Key,
                            MediaEstrelas = _.Average(_ => _.Estrelas)
                        })
                    .OrderByDescending(m => m.MediaEstrelas)
                    .Take(3)
                    .ToList();

            top3Avaliacoes.ForEach(item =>
            {
                var restaurante = ObterPorId(item.RestauranteId);

                _avaliacoes.AsQueryable()
                .Where(_ => _.RestauranteId == item.RestauranteId)
                .ToList()
                .ForEach(a => restaurante.InserirAvaliacao(a.ConverterParaDomain()));

                retorno.Add(restaurante, item.MediaEstrelas);
            });

            return retorno;
        }

        public (long, long) Remover(string restauranteId)
        {
            var resultadosAvaliacoes = _avaliacoes.DeleteMany(_ => _.RestauranteId.Equals(restauranteId));
            var resultadoRestaurante = _restaurantes.DeleteOne(_ => _.Id.Equals(restauranteId));

            return (resultadosAvaliacoes.DeletedCount, resultadoRestaurante.DeletedCount);
        }
    }
}
