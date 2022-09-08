using MongoDotNet.API.Domain.Enums;

namespace MongoDotNet.API.Data.Schemas
{
    public class RestauranteSchema
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public ETipoDeComida TipoDeComida { get; set; }
        public EnderecoSchema EnderecoSchema { get; set; }
    }
}
