using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Report.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration = null)
        {
        }
    }
}
