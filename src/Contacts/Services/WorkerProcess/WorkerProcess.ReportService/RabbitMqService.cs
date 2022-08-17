using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace WorkerProcess.ReportService
{
    public class RabbitMqService
    {
        private readonly IConnectionFactory _connection;

        public RabbitMqService()
        {
            _connection = GetRabbirMqConnectionFactory();
        }

        public IConnectionFactory GetRabbirMqConnectionFactory()
        {
            return new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Consume()
        {
            using (IConnection connection = _connection.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //channel.QueueDeclare("fanout.loggerWorker", false, false, false);
                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                    MessageQueueType message = null;
                    string messageString = string.Empty; 
                    string path = string.Empty;

                    consumer.Received += (sender, args) =>
                    {
                        try
                        {
                            messageString = Encoding.UTF8.GetString(args.Body.ToArray());
                            message = JsonSerializer.Deserialize<MessageQueueType>(messageString);
                            path = @"C:\Users\secevit\source\repos\assessment-backend-net\src\Contacts\Services\WorkerProcess\WorkerProcess.ReportService\report.txt";
                            File.AppendAllLines(path, new string[] { message.UUID.ToString() + " - ", message.RequestDate.ToString() + " - ", message.StatusID.ToString() });
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.StackTrace.ToString());
                        }
                    };
                    channel.BasicConsume("fanout.loggerWorker", true, consumer);
                }
            }
        }
    }
}
