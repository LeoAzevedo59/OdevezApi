using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.Interfaces;
using Odevez.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Business
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;
        private readonly IAutenticarBusiness _autenticarBusiness;

        public ClientController(IClientBusiness clientBusiness, IAutenticarBusiness autenticarBusiness)
        {
            _clientBusiness = clientBusiness;
            _autenticarBusiness = autenticarBusiness;
        }

        [HttpGet]
        [Route("obter")]
        public async Task<IActionResult> ListByFilterAsync([Range(0, Int32.MaxValue, ErrorMessage = "O campo {0}, precisa ser maior ou igual a {1}.")] int clientId, string name = null)
        {
            try
            {
                var retorno = await _clientBusiness.ListByFilterAsync(clientId, name);

                if (retorno != null && retorno.Count() > 0)
                    return Ok(retorno);

                return BadRequest("Usuário não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> InserirClient([FromBody] ClientDTO client)
        {
            try
            {
                bool usuarioJaCadastrado = await _clientBusiness.VerifyPhoneNumber(client.PhoneNumber);
                if (usuarioJaCadastrado)
                    return BadRequest("Número de celular já utilizado.");

                var retorno = await _clientBusiness.InserirClient(client);

                if (retorno)
                    return Ok("Usuário cadastrado com sucesso.");

                return BadRequest("Erro");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginClient([Required, Range(0, Int64.MaxValue, ErrorMessage = "O campo {0}, precisa ser maior ou igual a {1}.")] long phoneNumber, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                    return BadRequest("Senha é obrigatório.");

                var retorno = await _autenticarBusiness.LoginClient(phoneNumber, password);

                if (retorno != null)
                    return Ok(retorno);

                return BadRequest("Senha incorreta ou usário não cadastrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
