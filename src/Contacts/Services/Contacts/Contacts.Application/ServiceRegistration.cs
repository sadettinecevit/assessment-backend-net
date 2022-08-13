using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection service, IConfiguration configuration = null)
        {
        }
    }
}
