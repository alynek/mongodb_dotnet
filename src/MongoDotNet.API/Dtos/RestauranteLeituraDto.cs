namespace MongoDotNet.API.Dtos
{
    public class RestauranteLeituraDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string TipoComida { get; set; }
        public EnderecoLeituraDto Endereco { get; set; }
    }
}
