using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

var quequeName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: quequeName, exchange: "logs", routingKey: string.Empty);

Console.WriteLine(" [*] Waiting for logs.");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[x] {message}");
};

channel.BasicConsume(queue: quequeName, autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");

Console.ReadLine();