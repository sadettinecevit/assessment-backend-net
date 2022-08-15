using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Report.Application;
using Report.Application.Interfaces.Repositories;
using Report.Persistence.Context;
using Report.Persistence.Repository;

namespace Report.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration?.GetConnectionString("Default"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddTransient<IReportLogRepository, ReportLogRepository>();
            services.AddTransient<IReportLogStatusRepository, ReportLogStatusRepository>();

            services.AddApplicationService(configuration);
        }
    }
}
