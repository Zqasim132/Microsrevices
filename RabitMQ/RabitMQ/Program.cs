using RabbitMQ.Client;
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


//Exchange Creation
channel.ExchangeDeclare("ex.fanout", "fanout", true, false, null);

//Queues creation
channel.QueueDeclare("Queue_1",true,false,false, null);
channel.QueueDeclare("Queue_2",true,false,false, null);

//Queues binding
channel.QueueBind("Queue_1", "ex.fanout", "", null);
channel.QueueBind("Queue_2", "ex.fanout", "", null);

//Publishing message
channel.BasicPublish("ex.fanout", "", null, Encoding.UTF8.GetBytes("First Message..!"));
channel.BasicPublish("ex.fanout", "", null, Encoding.UTF8.GetBytes("Second Message..!"));


////deleting
//channel.QueueDelete("Queue_1");
//channel.QueueDelete("Queue_2");
//channel.ExchangeDelete("ex.fanout");

////closing
//channel.Close();
//connection.Close();
