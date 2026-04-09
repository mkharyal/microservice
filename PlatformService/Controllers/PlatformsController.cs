using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController(
    IPlatformRepo repo, 
    IMapper mapper, 
    ICommandDataClient commandDataClient,
    IMessageBusClient messageBusClient) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
    {
        Console.WriteLine($"---> Getting Platforms from {repo.GetType()}");

        var platformItems = repo.GetAllPlatforms();
        return Ok(mapper.Map<IEnumerable<PlatformReadDTO>>(platformItems));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDTO> GetPlatformById(int id)
    {
        var platformItem = repo.GetPlatformById(id);
        if (platformItem == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<PlatformReadDTO>(platformItem));
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platformCreateDTO)
    {
        var platformModel = mapper.Map<Models.Platform>(platformCreateDTO);
        repo.CreatePlatform(platformModel);
        repo.SaveChanges();

        var platformReadDTO = mapper.Map<PlatformReadDTO>(platformModel);

        // Send Async
        try
        {
            await commandDataClient.SendPlatformToCommand(platformReadDTO);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
        }

        // Send Async
        try
        {
            var platformPublishedDto = mapper.Map<PlatformPublishedDto>(platformReadDTO);
            platformPublishedDto.Event = "Platform_Published";
            await messageBusClient.PublishNewPlatformAsync(platformPublishedDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
        }

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDTO.Id }, platformReadDTO);
    }
}