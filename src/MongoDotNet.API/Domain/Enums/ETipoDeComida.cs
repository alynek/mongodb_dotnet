namespace MongoDotNet.API.Domain.Enums
{
    public enum ETipoDeComida
    {
        Brasileira = 1,
        Italiana = 2,
        Arabe = 3,
        Japonesa = 4,
        FastFood = 5
    }

    public static class TipoDeComidaHelper
    {
        public static ETipoDeComida ConverterDeInteiro(int valor)
        {
            if (Enum.TryParse(valor.ToString(), out ETipoDeComida tipoDeComida)) return tipoDeComida;

            throw new ArgumentOutOfRangeException("Não foi possível converter de int para enum");
        }
    }
}
