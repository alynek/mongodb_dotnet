using Microsoft.AspNetCore.Mvc;
using MongoDotNet.API.Data.Repositories;
using MongoDotNet.API.Domain.Enums;
using MongoDotNet.API.Dtos;

namespace MongoDotNet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public RestauranteController(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        [HttpPost("novo-restaurante")]
        public ActionResult AdicionarRestaurante([FromBody] NovoRestauranteDto novoRestauranteDto)
        {
            var tipoComida = TipoDeComidaHelper.ConverterDeInteiro(novoRestauranteDto.TipoDeComida);

            var restaurante = novoRestauranteDto.NovoRestaurante(tipoComida);
            var endereco = novoRestauranteDto.NovoEndereco();

            restaurante.AtribuirEndereco(endereco);

            if (!restaurante.Validar())
            {
                return BadRequest(new { errors = restaurante.ValidationResult.Errors.Select(e => e.ErrorMessage) });
            }

            _restauranteRepository.Inserir(restaurante);
            return Ok(new { data = "Restaurante inserido com sucesso" });
        }

        [HttpGet("restaurante/todos")]
        public async Task<ActionResult> ObterRestaurantes()
        {
            var restaurantes = await _restauranteRepository.ObterTodos();

            var restaurateDto = restaurantes.Select(_ => new RestauranteLeituraDto
            {
                Id = _.Id,
                Nome = _.Nome,
                TipoComida = _.TipoComida.ToString(),
                Cidade = _.Endereco.Cidade
            });

            return Ok(new { data = restaurateDto });
        }
    }
}
