using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Permissions.Application.Mappings;
using Astrum.Permissions.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Astrum.Permissions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var currentAssembly = typeof(PermissionsProfile).Assembly;
            services.AddScoped<IPermissionSectionService, PermissionSectionService>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(currentAssembly);
            });
        }
    }
}
