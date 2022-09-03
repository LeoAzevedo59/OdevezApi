using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Interfaces;
using System.Threading.Tasks;

namespace Odevez.Business
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [HttpGet]
        [Route("obter")]
        public async Task<IActionResult> ListByFilterAsync(int? clientId = 0, string name = null)
        {
            var retorno = await _clientBusiness.ListByFilterAsync(clientId, name);
            return Ok(retorno);
        }

    }
}
