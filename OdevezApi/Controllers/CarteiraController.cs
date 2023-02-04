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

        [HttpGet]
        [Route("obter-tipo-carteira")]
        public async Task<IActionResult> ObterTipoCarteira()
        {
            var retorno = new List<TipoCarteiraViewModel>();
            retorno = await _carteiraBusiness.ObterTipoCarteira();

            return Ok(retorno);
        }

        [HttpPost]
        [Route("incluirTipo")]
        public async Task<IActionResult> IncluirTipoCarteira([FromBody] TipoCarteiraDTO tipoCarteira)
        {
            bool retorno = await _carteiraBusiness.IncluirTipo(tipoCarteira);

            return Ok(retorno);
        }

        [HttpPost]
        [Route("incluir")]
        public async Task<IActionResult> Incluir([FromBody] CarteiraDTO carteira)
        {
            bool retorno = await _carteiraBusiness.Incluir(carteira);
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-carteira-por-usuario")]
        public async Task<IActionResult> ObterCarteira(int usuario, int tipoCarteira)

        {
            var retorno = await _carteiraBusiness.ObterCarteira(usuario, tipoCarteira);
            return Ok(retorno);
        }

        [HttpDelete]
        [Route("excluir-carteira")]
        public async Task<IActionResult> ExcluirCarteira(int usuario, int carteira)
        {
            await _carteiraBusiness.ExcluirCarteira(usuario, carteira);
            return Ok();
        }

        [HttpGet]
        [Route("obter-valor-por-tipo")]
        public async Task<IActionResult> ObterValorPorTipo(int tipoCarteira, int usuario)
        {
            decimal retorno = await _carteiraBusiness.ObterValorPorTipo(tipoCarteira, usuario);
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-valor-por-usuario")]
        public async Task<IActionResult> ObterValorPorUsuario(int usuario)
        {
            decimal retorno = await _carteiraBusiness.ObterValorPorUsuario(usuario);
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-valor-por-codigo")]
        public async Task<IActionResult> ObterValorPorCodigo(int carteira)
        {
            decimal retorno = await _carteiraBusiness.ObterValorPorCodigo(carteira);
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter")]
        public async Task<IActionResult> Obter()
        {
            var retorno = await _carteiraBusiness.Obter();
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-por-codigo")]
        public async Task<IActionResult> ObterPorCodigo(int carteira, int usuario)
        {
            var retorno = await _carteiraBusiness.ObterPorCodigo(carteira, usuario);
            return Ok(retorno);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> Alterar([FromBody] CarteiraDTO carteiraDTO)
        {
            var retorno = await _carteiraBusiness.Alterar(carteiraDTO);
            return Ok(retorno);
        }
    }
}
