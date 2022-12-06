using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.API.ViewModel;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using Odevez.Utils.Enum;
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
            var retorno = new List<CarteiraExtratoViewModel>();
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

        [HttpGet]
        [Route("obter-categoria-carteira-por-usuario")]
        public async Task<IActionResult> ObterCategoriaCarteiraPorUsuario(int usuario)
        {
            var retorno = new List<CategoriaExtratoViewModel>();
            retorno = await _carteiraBusiness.ObterCategoriaCarteiraPorUsuario(usuario);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("incluir-movimentacao-carteira")]
        public async Task<IActionResult> IncluirTransacaoCarteira([FromBody] ExtratoViewModel extratoViewModel)
        {
            bool retorno = await _carteiraBusiness.IncluirTransacaoCarteira(extratoViewModel);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-tipo-carteira")]
        public async Task<IActionResult> ObterTipoCarteira()
        {
            var retorno = new List<TipoCarteiraViewModel>();
            retorno = await _carteiraBusiness.ObterTipoCarteira();

            return Ok(retorno);
        }

        [HttpPost]
        [Route("incluir-tipo-carteira")]
        public async Task<IActionResult> IncluirTipoCarteira([FromBody] TipoCarteiraDTO tipoCarteira)
        {
            bool retorno = await _carteiraBusiness.IncluirTipoCarteira(tipoCarteira);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-carteira-por-usuario")]
        public async Task<IActionResult> ObterCarteira(int usuario, int tipoCarteira)

        {
            var retorno = await _carteiraBusiness.ObterCarteira(usuario, tipoCarteira);
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-valor-carteira-por-tipo-carteira")]
        public async Task<IActionResult> ObterValorCarteiraPorTipoCarteira(int usuario, int tipoCarteira)
        {
            decimal retorno = await _carteiraBusiness.ObterValorCarteiraPorTipoCarteira(usuario, tipoCarteira);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("excluir-carteira")]
        public async Task<IActionResult> ExcluirCarteira(int usuario, int carteira)
        {
            await _carteiraBusiness.ExcluirCarteira(usuario, carteira);
            return Ok();
        }
    }
}
