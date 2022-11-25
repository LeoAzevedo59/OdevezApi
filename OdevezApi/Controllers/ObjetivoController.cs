using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Odevez.API.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Authorize]
    public class ObjetivoController : ControllerBase
    {
        
    }
}
