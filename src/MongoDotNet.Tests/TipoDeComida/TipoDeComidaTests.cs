using MongoDotNet.API.Domain.Enums;

namespace MongoDotNet.Tests.TipoDeComida
{
    public class TipoDeComidaTests
    {
        [Theory]
        [InlineData(1, ETipoDeComida.Brasileira)]
        [InlineData(2, ETipoDeComida.Italiana)]
        [InlineData(3, ETipoDeComida.Arabe)]
        [InlineData(4, ETipoDeComida.Japonesa)]
        [InlineData(5, ETipoDeComida.FastFood)]
        public void ConverterDeInteiro_DeveRetornarUmTipoDeComida(int valor, ETipoDeComida tipoComidaParaComparar)
        {
            var tipoComida = TipoDeComidaHelper.ConverterDeInteiro(valor);

            Assert.Equal(tipoComida, tipoComidaParaComparar);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        public void ConverterDeInteiro_DeveRetornarExceptionSeTipoComidaInvalido(int valor)
        {
            var exception = Assert.Throws<ArgumentException>(() => TipoDeComidaHelper.ConverterDeInteiro(valor));

            Assert.Equal("Não foi possível converter de int para enum", exception.Message);
        }
    }
}
