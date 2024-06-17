using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Permissions.DomainServices.Repositories;
using Astrum.Permissions.Persistence;
using Astrum.Permissions.Persistence.Repositories;
using Astrum.SharedLib.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Astrum.Permissions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDbContext<PermissionsDbContext>(configuration.GetConnectionString("BaseConnection"));
            services.AddScoped<IPermissionSectionsRepository, PermissionSectionsRepository>();
        }
    }
}
