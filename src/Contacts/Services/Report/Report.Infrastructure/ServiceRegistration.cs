using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Report.Infrastructure.Services.MessageQueue;

namespace Report.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
        }
    }
}
