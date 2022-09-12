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

            var restaurateDto = restaurantes.Select(_ => new RestaurantesLeituraDto
            {
                Id = _.Id,
                Nome = _.Nome,
                TipoComida = _.TipoComida.ToString(),
                Cidade = _.Endereco.Cidade
            });

            return Ok(new { data = restaurateDto });
        }

        [HttpGet("restaurante/{id}")]
        public ActionResult ObterRestaurante(string id)
        {
            if (IdNaoEhValido(id)) return BadRequest();

            var restaurante = _restauranteRepository.ObterPorId(id);

            if (restaurante is null) return NotFound();

            var restauranteDto = new RestauranteLeituraDto
            {
                Id = restaurante.Id,
                Nome = restaurante.Nome,
                TipoComida = restaurante.TipoComida.ToString(),
                Endereco = new EnderecoLeituraDto
                {
                    Logradouro = restaurante.Endereco.Logradouro,
                    Numero = restaurante.Endereco.Numero,
                    Cidade = restaurante.Endereco.Cidade,
                    Cep = restaurante.Endereco.Cep,
                    UF = restaurante.Endereco.UF
                }
            };

            return Ok(restauranteDto);
        }

        [HttpPut("restaurante")]
        public ActionResult AlterarRestaurante([FromBody] RestauranteAlteracaoDto restauranteDto)
        {
            if (restauranteDto is null) return NotFound();
            if (IdNaoEhValido(restauranteDto.Id)) return BadRequest();

            var restaurante = restauranteDto.NovoRestaurante();

            if (!restaurante.Validar()) return BadRequest(new
            {
                errors = restaurante.ValidationResult.Errors.Select(_ => _.ErrorMessage)
            });

            if (!_restauranteRepository.AlterarRestaurante(restaurante)) return BadRequest("Nenhum restaurante foi alterado");
            return Ok(new { data = "Restaurante alterado com sucesso!" });
        }

        private bool IdNaoEhValido(string id)
        {
            int tamanhoCorretoDoId = 24;
            return id.Length != tamanhoCorretoDoId;
        }
    }
}
