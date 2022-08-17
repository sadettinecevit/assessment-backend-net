using RabbitMQ.Client;
using Report.Domain.Entities;

namespace Report.Infrastructure.Services.MessageQueue
{
    public interface IRabbitMqService
    {
        IConnectionFactory GetRabbirMqConnectionFactory();
        void Puslish(ReportLog message);
    }
}
