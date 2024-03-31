using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var conenction = factory.CreateConnection();
using var channel = conenction.CreateModel();

channel.ExchangeDeclare(
    exchange: "logs",
    type: ExchangeType.Fanout
);

var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);


channel.BasicPublish(
    exchange: "logs",
    routingKey: string.Empty,
    basicProperties: null,
    body: body
);

Console.WriteLine($"[x] Sent {message}");

Console.WriteLine("Press [enter] to exit.");

Console.ReadLine();
static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join("", args): "info: Hello World!");
}