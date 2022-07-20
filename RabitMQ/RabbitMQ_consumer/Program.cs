using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

IConnection connection;
IModel channel;
ConnectionFactory connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
    VirtualHost = "/",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};
connection = connectionFactory.CreateConnection();
channel = connection.CreateModel();

//for setting the messages according to their priority
channel.BasicQos(0,1,false);

//creating consumer object
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

//attaching the function to event handler
consumer.Received += (sender, args) => {
    string message = Encoding.UTF8.GetString(args.Body.Span);
    Console.WriteLine("Message is: {0}",message);
    channel.BasicAck(args.DeliveryTag,false);
};

var consumerTag = channel.BasicConsume("Queue_2",false, consumer);

Console.WriteLine("Waiting for messages. Press any key to exit. ");
Console.ReadKey();
