using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.ViewModel;
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
            bool retorno = await _extratoBsuiness.IncluirTransacaoCarteira(extratoViewModel);

            return Ok(retorno);
        }

        [HttpDelete]
        [Route("excluir-extrato")]
        public async Task<IActionResult> ExcluirExtrato(int extrato, int carteira)
        {
            await _extratoBsuiness.ExcluirExtrato(extrato, carteira);
            return Ok();
        }
    }
}
