using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> ListByFilterAsync([Range(0, Int32.MaxValue, ErrorMessage = "O campo {0}, precisa ser maior ou igual a {1}.")] int clientId, string name = null)
        {
            try
            {
                var retorno = await _clientBusiness.ListByFilterAsync(clientId, name);

                if (retorno != null)
                    return Ok(retorno);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
