using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Report.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection service, IConfiguration configuration = null)
        {
        }
    }
}
