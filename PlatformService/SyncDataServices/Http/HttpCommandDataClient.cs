using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration) : ICommandDataClient
{
    public async Task SendPlatformToCommand(PlatformReadDTO platform)
    {
        var httpContent = new StringContent(
             JsonSerializer.Serialize(platform),
             Encoding.UTF8,
             "application/json"
         );

        var response = await httpClient.PostAsync(configuration["CommandService"], httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to CommandService was OK!");
        }
        else
        {
            Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
        }
    }
}