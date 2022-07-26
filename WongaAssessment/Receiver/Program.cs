using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Text;

namespace Receiver
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Could not set a User Name and Password for the HostName, because I can't access the (RabbitMQ Management)
            var factory = new ConnectionFactory() { HostName = "" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Hello, " + message + " I am your father!");
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Please press ENTER to exit.");
                Console.ReadLine();
            }
        }
    }
}
