using System.Text;
using CommandsService.EventProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService
{
    private IConnection _connection = null!;
    private IChannel _channel = null!;
    private string _queueName = null!;
    private IConfiguration _configuration;
    private IEventProcessor _eventProcessor;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;
        
        IntitializeRabbitMQ();
    }
    private void IntitializeRabbitMQ()
    {
        IConnectionFactory factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"]!,
            Port = int.Parse(_configuration["RabbitMQPort"]!)
        };

        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout);

        _queueName = _channel.QueueDeclareAsync().Result.QueueName;

        _channel.QueueBindAsync(queue: _queueName,
            exchange: "trigger",
            routingKey: "");

        Console.WriteLine("--> Listening on the Message Bus...");

        _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutdown;
    }

    private async Task RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs @event)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (ModuleHandle, ea) =>
        {
            Console.WriteLine("--> Event Received!");

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _eventProcessor.ProcessEvent(message);
        };

        _channel.BasicConsumeAsync(queue: _queueName,
            autoAck: true,
            consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
        }

        base.Dispose();
    }
}