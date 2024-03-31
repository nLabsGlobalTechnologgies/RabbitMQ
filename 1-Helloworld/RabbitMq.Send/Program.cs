using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "hello", // kuyruk
    durable: true, // rabbitmq kapandıgında hayatta kal
    exclusive: false, // özel kuyruk oluşsunmu
    autoDelete: false, // baglı consume yoksa kuyrugu sil
    arguments: null // özel argümanlar
);

const string message = "Hello world";

var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(
    exchange: string.Empty, // directive / topic / fonut ve headers
    routingKey: "hello", // baglanılacak kuyruk
    basicProperties: null, // temel özellikler
    body: body // gönderilecek message
);

Console.WriteLine($"[x] send {message}");

Console.WriteLine($"Press [Enter] to exit.");

Console.ReadLine();