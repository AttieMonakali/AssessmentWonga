using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Text;

namespace Sender
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


                // Leaves a blank line
                Console.WriteLine();

                // Request for user input
                Console.WriteLine("Please enter your name: ");

                // Create a string variable called NAME and get user input
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name can't be empty! Input your name once more");

                    // Leaves a blank line
                    Console.WriteLine();

                    // Request for user input
                    Console.WriteLine("Please enter your name: ");
                    name = Console.ReadLine();
                }
                // Leaves a blank line
                Console.WriteLine();

                string message = name;
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

                // Prints/Displays the variable that shows the input from the user
                Console.WriteLine("Sent: " + message);

                // Leaves a blank line
                Console.WriteLine();
            }

            Console.WriteLine("Please press ENTER to exit.");
            Console.ReadLine();
        }
    }
}
