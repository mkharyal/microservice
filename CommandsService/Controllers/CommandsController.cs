using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers

{
    [ApiController]
    [Route("api/cmd/platforms/{platformId}/[controller]")]
    public class CommandsController(ICommandRepo commandRepo, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if (!commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = commandRepo.GetCommandsForPlatform(platformId);
            System.Console.WriteLine($"--> Retrieved {commands.Count()} commands for platform {platformId}");
            return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId}, {commandId}");

            if (!commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = commandRepo.GetCommand(platformId, commandId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"--> Hit CreateCommand: {platformId}");

            if (!commandRepo.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = mapper.Map<Command>(commandCreateDto);
            commandRepo.CreateCommand(platformId, command);
            System.Console.WriteLine($"--> [Before] Created command for platform {platformId}");
            commandRepo.SaveChanges();
            System.Console.WriteLine($"--> [After] Created command for platform {platformId}");


            var commandReadDto = mapper.Map<CommandReadDto>(command);
            System.Console.WriteLine($"--> [After Mapping] Created command for platform {platformId} : {commandReadDto.Id}");

            return CreatedAtRoute("GetCommandForPlatform", new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
        }
    }

}