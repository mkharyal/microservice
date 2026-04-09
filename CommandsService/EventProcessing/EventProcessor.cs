using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.EventProcessor
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEventType(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    System.Console.WriteLine("--> Could not determine the event type");
                    break;
            }
        }

        private void AddPlatform(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(message);

            try
            {
                var platform = _mapper.Map<Platform>(platformPublishedDto);

                if (!repo.ExternalPlatformExists(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                    repo.SaveChanges();
                    System.Console.WriteLine("--> Platform added!");
                }
                else
                {
                    System.Console.WriteLine("--> Platform already exists...");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"--> Could not add Platform to DB: {ex.Message}");
            }
        }

        private EventType DetermineEventType(string notificationMessage)
        {
            System.Console.WriteLine("--> Determining Event Type");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            return eventType?.Event switch
            {
                "Platform_Published" => EventType.PlatformPublished,
                _ => EventType.Undetermined
            };
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}