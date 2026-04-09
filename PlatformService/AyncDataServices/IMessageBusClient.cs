using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient : IDisposable
    {
        Task PublishNewPlatformAsync(PlatformPublishedDto platformPublishedDto);
    }
}