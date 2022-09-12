using MongoDotNet.API.Domain.Entities;
using MongoDotNet.API.Domain.Enums;
using MongoDotNet.API.Domain.ValueObjects;

namespace MongoDotNet.API.Dtos
{
    public class RestauranteAlteracaoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int TipoComida { get; set; }
        public EnderecoLeituraDto Endereco { get; set; }

        public Restaurante NovoRestaurante()
        {
            var comida = TipoDeComidaHelper.ConverterDeInteiro(TipoComida);

            var restaurante = new Restaurante(Id, Nome, comida);
            var endereco = new Endereco(Endereco.Logradouro, Endereco.Numero, Endereco.Cidade, Endereco.UF, Endereco.Cep);
            restaurante.AtribuirEndereco(endereco);
            return restaurante;
        }
    }
}
