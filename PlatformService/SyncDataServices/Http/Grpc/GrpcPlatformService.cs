using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService(IPlatformRepo platformRepo, IMapper mapper) : GrpcPlatform.GrpcPlatformBase
    {
        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = platformRepo.GetAllPlatforms();

            foreach (var platform in platforms)
            {
                response.Platform.Add(mapper.Map<GrpcPlatformModel>(platform));
            }

            return Task.FromResult(response);
        }
    }
}