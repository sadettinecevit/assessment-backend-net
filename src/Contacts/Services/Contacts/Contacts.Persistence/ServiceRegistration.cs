using Contacts.Application;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Application.Interfaces.UnitOfWork;
using Contacts.Persistence.Context;
using Contacts.Persistence.Repository;
using Contacts.Persistence.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration?.GetConnectionString("Default"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactInfoRepository, ContactInfoRepository>();
            services.AddTransient<IInfoTypeRepository, InfoTypeRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplicationService(configuration);
        }
    }
}
