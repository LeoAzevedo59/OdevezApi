using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.API.ViewModel;
using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.API.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class CarteiraController : ControllerBase
    {
        private readonly ICarteiraBusiness _carteiraBusiness;
        public CarteiraController(ICarteiraBusiness carteiraBusiness)
        {
            _carteiraBusiness = carteiraBusiness;
        }

        [HttpGet]
        [Route("obter-valor-carteira-por-usuario")]
        public async Task<IActionResult> ObterValorCarteiraPorUsuario(int usuario)
        {
            decimal retorno = await _carteiraBusiness.ObterValorCarteiraPorUsuario(usuario);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-descricao-carteira-por-usuario")]
        public async Task<IActionResult> ObterDescricaoCarteiraPorUsuario(int usuario)
        {
            var retorno = new List<CarteiraDescricaoViewModel>();
            retorno = await _carteiraBusiness.ObterDescricaoCarteiraPorUsuario(usuario);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-movimentacao-carteira")]
        public async Task<IActionResult> ObterMovimentacaoCarteira()
        {
            var retorno = new List<MovimentacaoDTO>();
            retorno = await _carteiraBusiness.ObterMovimentacaoCarteira();

            return Ok(retorno);
        }
    }
}
