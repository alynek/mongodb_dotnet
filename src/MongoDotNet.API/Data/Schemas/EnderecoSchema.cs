namespace MongoDotNet.API.Data.Schemas
{
    public class EnderecoSchema
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Cep { get; set; }

        public EnderecoSchema(string logradouro, string numero, string cidade, string uF, string cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Cidade = cidade;
            UF = uF;
            Cep = cep;
        }
    }
}
