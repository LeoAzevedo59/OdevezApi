using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Interfaces;
using Odevez.DTO;
using Odevez.Utils.Enum;
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

        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> InserirClient([FromBody] ClientDTO client)
        {
            try
            {
                var retorno = await _clientBusiness.InserirClient(client);

                if (retorno)
                    return Ok(ResponseMessageEnum.Sucesso.ToString());

                return BadRequest(ResponseMessageEnum.Ja_Existente_No_Banco.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> LoginClient([Range(0, Int64.MaxValue, ErrorMessage = "O campo {0}, precisa ser maior ou igual a {1}.")] long phoneNumber, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                    return BadRequest(ResponseMessageEnum.Existe_campo_vazio.ToString());

                var retorno = await _clientBusiness.LoginClient(phoneNumber, password);

                if (retorno)
                    return Ok(ResponseMessageEnum.Sucesso.ToString());

                return BadRequest(ResponseMessageEnum.Ja_Existente_No_Banco.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
