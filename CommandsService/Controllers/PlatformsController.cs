using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;


[Route("api/cmd/[controller]")]
[ApiController]
public class PlatformsController(ICommandRepo commandRepo, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inbound POST # Command Service");

        return Ok("Inbound test of from Platforms Controller");
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms from Command Service");

        var platformItems = commandRepo.GetAllPlatforms();
        System.Console.WriteLine($"--> Retrieved {platformItems.Count()} platforms from Command Service");
        return Ok(mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }
}