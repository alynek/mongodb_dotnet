﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDotNet.API.Domain.Entities;
using MongoDotNet.API.Domain.Enums;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Data.Schemas
{
    public class RestauranteSchema
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public ETipoDeComida TipoDeComida { get; set; }
        public EnderecoSchema Endereco{ get; set; }

        public Restaurante ConverterParaDomain()
        {
            var restaurante = new Restaurante(Id, Nome, TipoDeComida);
            var endereco = new Endereco(Endereco.Logradouro, Endereco.Numero,
                Endereco.Cidade, Endereco.UF, Endereco.Cep);

            restaurante.AtribuirEndereco(endereco);

            return restaurante;
        }
    }
}
