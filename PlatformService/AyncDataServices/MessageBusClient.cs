using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlatformService.AsyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
        _configuration = configuration;

        var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"]!, Port = int.Parse(_configuration["RabbitMQPort"]!) };

        try
        {
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;

            Console.WriteLine("--> Connected to Message Bus");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not connect to the Message Bus {ex.Message}");
        }
    }

    private async Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs @event)
    {
        Console.WriteLine($"--> Connection to Message Bus lost: {@event.ReplyText}");
    }

    public async Task PublishNewPlatformAsync(PlatformPublishedDto platformPublishedDto)
    {
        var message = JsonSerializer.Serialize(platformPublishedDto);

        if (_connection.IsOpen)
        {
            Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
            await SendMessage(message);
        }
        else
        {
            Console.WriteLine("--> RabbitMQ Connection Closed, not sending");
        }
    }

    private async Task SendMessage(string message)
    {
        var body = System.Text.Encoding.UTF8.GetBytes(message);

        await _channel.BasicPublishAsync(exchange: "trigger",
                             routingKey: "",
                             body: new System.ReadOnlyMemory<byte>(body));

        Console.WriteLine($"--> We have sent {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("MessageBus Disposed");
        if (_channel.IsOpen)
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
        }
    }
}

