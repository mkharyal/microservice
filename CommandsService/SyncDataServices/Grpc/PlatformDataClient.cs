using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
    public class PlatformDataClient(IConfiguration configuration, IMapper mapper) : IPlatformDataClient
    {
        public Task<IEnumerable<Platform>> ReturnAllPlatforms()
        {
            System.Console.WriteLine($"--> Calling GRPC Service {configuration["GrpcPlatform"]}");

            var channel = GrpcChannel.ForAddress(configuration["GrpcPlatform"]!);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);

            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPlatforms(request);
                return Task.FromResult(mapper.Map<IEnumerable<Platform>>(reply.Platform));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"--> Could not call GRPC Server: {ex.Message}");
                return Task.FromResult<IEnumerable<Platform>>(new List<Platform>());
            }
        }
    }

}