using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDotNet.API.Domain.Enums;

namespace MongoDotNet.API.Data.Schemas
{
    public class RestauranteSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public ETipoDeComida TipoDeComida { get; set; }
        public EnderecoSchema Endereco{ get; set; }
    }
}
