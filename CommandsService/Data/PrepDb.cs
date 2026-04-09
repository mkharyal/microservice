using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

            var platforms = grpcClient!.ReturnAllPlatforms().GetAwaiter().GetResult();
            System.Console.WriteLine($"---> Found {platforms.Count()} platform(s) in the gRPC service.");
            SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>()!, platforms);
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            System.Console.WriteLine($"---> Seeding new platforms...");

            foreach (var plat in platforms)
            {
                if (!repo.ExternalPlatformExists(plat.ExternalId))
                {
                    System.Console.WriteLine($"---> Seeding platform {plat.Name} with External ID {plat.ExternalId}");
                    repo.CreatePlatform(plat);
                    repo.SaveChanges();
                }
            }
        }
    }
}