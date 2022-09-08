using MongoDotNet.API.Domain.Entities;
using MongoDotNet.API.Domain.Enums;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Dtos
{
    public class NovoRestauranteDto
    {
        public string Nome { get; set; }
        public int TipoDeComida { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Cep { get; set; }

        public Restaurante NovoRestaurante(ETipoDeComida tipoDeComida)
        {
            return new Restaurante(Nome, tipoDeComida);
        }

        public Endereco NovoEndereco()
        {
            return new Endereco(Logradouro, Numero, Cidade, UF, Cep);
        }
    }
}
