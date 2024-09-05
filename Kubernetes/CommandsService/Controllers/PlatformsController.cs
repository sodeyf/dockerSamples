using CommandsService.Heplers;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ILogger<PlatformsController> _logger;

        public PlatformsController(ILogger<PlatformsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult TestInbountConnection()
        {
            Utils.Write("--> Inbound POST # Command Service");

            return Ok("Inbount test of from Platforms Controller");
        }
    }
}
