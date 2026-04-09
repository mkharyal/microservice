using CommandsService.Models;

namespace CommandsService.Data;

public class CommandRepo(AppDbContext context) : ICommandRepo
{
    public void CreateCommand(int platformId, Command command)
    {
        _ = command ?? throw new ArgumentNullException(nameof(command));
        command.Platform = null!;
        command.PlatformId = platformId;
        context.Commands.Add(command);
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform is null)
        {
            throw new ArgumentNullException(nameof(platform));
        }

        context.Platforms.Add(platform);
    }

    public bool ExternalPlatformExists(int externalPlatformId)
    {
        return context.Platforms.Any(p => p.ExternalId == externalPlatformId);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return [.. context.Platforms];
    }

    public Command? GetCommand(int platformId, int commandId)
    {
        return context.Commands
         .Where(c => c.PlatformId == platformId && c.Id == commandId)
         .FirstOrDefault();
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
        return context.Commands
        .Where(c => c.PlatformId == platformId)
        .OrderBy(c => c.Platform.Name);
    }

    public bool PlatformExists(int platformId)
    {
        return context.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChanges()
    {
        return (context.SaveChanges() >= 0);
    }
}