using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Business.Interfaces;
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
    }
}
