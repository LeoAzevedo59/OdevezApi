using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Interfaces;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [HttpGet]
        [Route("{clientId}")]
        public IActionResult GetByIdAsync(int clientId)
        {
            var client = new ClientDTO();
            //var retorno = await _clientBusiness.GetByIdAsync(clientId);
            return Ok(client);
        }

    }
}
