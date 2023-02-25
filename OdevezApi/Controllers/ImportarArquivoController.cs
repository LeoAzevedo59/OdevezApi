

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.API.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class ImportarArquivoController : ControllerBase
    {
        private readonly IImportarArquivoBusiness _importarArquivoBusiness;
        public ImportarArquivoController(IImportarArquivoBusiness importarArquivoBusiness)
        {
            _importarArquivoBusiness = importarArquivoBusiness;
        }

        [HttpPost]
        [Route("incluir-ofx")]
        public async Task<IActionResult> IncluirOFX([FromBody] ImportFileDTO file)
        {
            if (await _importarArquivoBusiness.IncluirOFX(file))
                return Ok("OFX incluido com sucesso.");
            else
                return BadRequest("Erro ao incluir OFX");
        }
    }
}
