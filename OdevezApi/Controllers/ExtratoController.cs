using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.API.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class ExtratoController : ControllerBase
    {
        private readonly IExtratoBsuiness _extratoBsuiness;

        public ExtratoController(IExtratoBsuiness extratoBsuiness)
        {
            _extratoBsuiness = extratoBsuiness;
        }

        [HttpGet]
        [Route("obter-extrato-resumido")]
        public async Task<IActionResult> ObterExtratoResumido(int usuario)
        {
            var retorno = await _extratoBsuiness.ObterExtratoResumido(usuario);
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-extrato")]
        public async Task<IActionResult> ObterExtrato(int usuario, string data, int carteira)
        {
            var retorno = await _extratoBsuiness.ObterExtrato(usuario, data, carteira);
            return Ok(retorno);
        }

        [HttpPost]
        [Route("incluir-movimentacao-carteira")]
        public async Task<IActionResult> IncluirTransacaoCarteira([FromBody] ExtratoViewModel extratoViewModel)
        {
            bool retorno = await _extratoBsuiness.Incluir(extratoViewModel);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("excluir-extrato")]
        public async Task<IActionResult> ExcluirExtrato(int extrato, int carteira)
        {
            await _extratoBsuiness.ExcluirExtrato(extrato, carteira);
            return Ok();
        }


        [HttpPut]
        [Route("alterar-status")]
        public async Task<IActionResult> AlterarStatus([FromBody] ExtratoStatusDTO extrato)
        {
            bool retorno = await _extratoBsuiness.AlterarStatus(extrato);
            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-extrato-por-codigo")]
        public async Task<IActionResult> ObterExtratoPorCodigo(int extrato)
        {
            var retorno = await _extratoBsuiness.ObterExtratoPorCodigo(extrato);
            return Ok(retorno);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> AlterarExtrato([FromBody] ExtratoViewModel extrato)
        {
            bool retorno = await _extratoBsuiness.AlterarExtrato(extrato);

            return Ok(retorno);
        }

        [HttpGet]
        [Route("obter-balanco-mensal")]
        public async Task<IActionResult> ObterBalancoMensal(int usuario)
        {
            var retorno = await _extratoBsuiness.ObterBalancoMensal(usuario);
            return Ok(retorno);
        }
    }
}
