namespace MongoDotNet.API.Dtos
{
    public class RestauranteTop3Dto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Cozinha { get; set; }
        public string Cidade { get; set; }
        public double Estrelas { get; set; }
    }
}
