using AutoMapper;
using CommandsService.Data;
using CommandsService.DTOs;
using CommandsService.Heplers;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Utils.Write("--> Getting Platforms from commandsService");

            var platformsItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platformsItems));
        }

        [HttpPost]
        public ActionResult TestInbountConnection()
        {
            Utils.Write("--> Inbound POST # Command Service");

            return Ok("Inbount test of from Platforms Controller");
        }
    }
}
