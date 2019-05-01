using System;
using System.Text;
using RabbitMQ.Client;

namespace Publisher
{

    //https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html

    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    //Cria uma nova queue se nao existir, ou abre a queue se existente
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine(" [x] Sent {0}", message);

                } //using

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            } //connection

        } //main

    } //class

} //namespace
