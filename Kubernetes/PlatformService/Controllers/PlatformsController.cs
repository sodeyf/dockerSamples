using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Heplers;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformsController(
            IPlatformRepo repository,
            IMapper mapper,
            ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        //[Route("GetPlatforms")]
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Utils.Write("--> Getting Platforms...");

            var platformItem = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platformItem));
        }


        [Route("{id}", Name = "GetPlatformById")]
        [HttpGet]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id)
        {
            Utils.Write("--> GetPlatformById...");

            var platformItem = _repository.GetAllPlatformById(id);

            if (platformItem != null)
                return Ok(_mapper.Map<PlatformReadDTO>(platformItem));

            return NotFound();
        }

        //[Route("{id}", Name = "GetPlatformById")]
        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDTO);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();


            var platformReadDTO = _mapper.Map<PlatformReadDTO>(platformModel);

            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDTO);
            }
            catch (Exception ex)
            {
                Utils.Write($"--> Could not send synchronosly: {ex.Message}");
            }

            return platformReadDTO;
            //return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDTO.Id });
        }
    }
}
