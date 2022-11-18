using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Business.Interfaces;
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
    }
}
