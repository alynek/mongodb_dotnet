using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDotNet.API.Data.Schemas;
using MongoDotNet.API.Domain.Enums;

namespace MongoDotNet.API.Data
{
    public class MongoDB
    {
        public IMongoDatabase MongoDatabase { get; }

        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration["ConnectionString"]);
                MongoDatabase = client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();
            }
            catch(Exception ex)
            {
                throw new MongoException($"Não foi possível se conectar ao MongoDB, erro: {ex}");
            }
        }

        private void MapClasses()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(RestauranteSchema)))
            {
                BsonClassMap.RegisterClassMap<RestauranteSchema>(i =>
                {
                    i.AutoMap();
                    i.MapIdMember(r => r.Id);
                    i.MapMember(r=> r.TipoDeComida).SetSerializer(new EnumSerializer<ETipoDeComida>(BsonType.Int32));
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}
